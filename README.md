# Hangfire Server
Server used to processes background jobs.The jobs are queued in a SQL Server instance (can be Redis or others). For my case, I used SQLExpress running on localhost.

# Hangfire Client
Client used to queue a background, recurring or delayed job. In our case, the background job is a running subscriber that is listening to published topics over rabbitmq.

# RabbitMQReceiver
Helper library that uses RabbitMQ.NET Client libraries for subscribing to a topic.
