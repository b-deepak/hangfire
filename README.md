# Hangfire 101
This project is a proof-of-concept application that uses hangfire.io (a background job scheduler for .NET comparable to Celery distributed task runner). The projects are built using .NET Core 1.1 platform.

# Projects

## Hangfire Server
Server used to processes background jobs.The jobs are queued in a SQL Server instance (can be Redis or others). For my case, I used SQLExpress running on localhost.

## Hangfire Client
Client used to queue a background, recurring or delayed job. In our case, the background job is a running subscriber that is listening to published topics over rabbitmq.

## RabbitMQReceiver
Helper library that uses RabbitMQ.NET Client libraries for subscribing to a topic.

# References

* [Hangfire.io Documentation](http://docs.hangfire.io/en/latest/index.html "Hangfire")
* [Background Tasks with hangfire.io](http://codeopinion.com/background-tasks/ "Background Tasks Example")

