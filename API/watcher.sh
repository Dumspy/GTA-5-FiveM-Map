while true; do
if ! screen -list | grep -q "watcher"; then
   echo 'Starting new watcher instance...'
   screen -dmS watcher yarn start
fi
sleep 5;
done;