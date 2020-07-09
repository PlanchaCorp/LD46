#!/bin/bash
# Thanks to Mor Shemesh for this extended timeout travis script

docker run -v "$(pwd):/root/project" gableroux/unity3d:2019.4.2f1-webgl xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' /opt/Unity/Editor/Unity -quit -batchmode -logFile /root/project/build.log -projectPath /root/project -executeMethod WebGLBuilder.build &

# Constants
RED='\033[0;31m'
minutes=0
limit=50

while kill -0 $! >/dev/null 2>&1; do
  echo -n -e " \b" # never leave evidences!

  if [ $minutes == $limit ]; then
    echo -e "\n"
    echo -e "${RED}Build has reached a ${minutes} minutes timeout limit"
    exit 1
  fi

  minutes=$((minutes+1))

  sleep 60
done

exit 0

