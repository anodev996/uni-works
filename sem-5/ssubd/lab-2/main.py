import redis

r = redis.Redis(host='127.0.0.1', port=6379)

r.flushdb()

r.set('username', 'Alice')

song = {
    "name": "Shot",
    "executor": "The Rasmus",
    "year": 2005,
    "duration": "4:18"
}
r.hset('albums', mapping=song)

r.rpush('tasks', 'task1', 'task2', 'task3')

print('\nFROM REDIS DB\n')
print(f'STRING username:\n      {r.get('username')}')
print(f'HASH albums:\n      {r.hgetall('albums')}', )
print(f'LIST tasks:\n      {r.lrange('tasks', 0, -1)}')
