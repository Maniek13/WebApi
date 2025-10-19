Api hosten by Azure:


Fronted: 

http://4.210.65.98:3000/

Endpoints

http://4.210.65.98:5000/StackOverFlow/Tags 

http://4.210.65.98:5000/StackOverFlow/RefreshData 

http://4.210.65.98:5000/App/Users/Register

http://4.210.65.98:5000/App/Users/Login

Websockets:

ws://4.210.65.98:5000/chat 

ws://4.210.65.98:5000/logs

RabbitMq: 

http://4.210.65.98:15672/

Hangfire dashbords:

http://4.210.65.98:5000/dashbord

http://4.210.65.98:5200/dashbord


Application run from docker:

 start command: docker-compose up

__________________________________________________________________________________________________________________________

Backend:


Endpoints:

1. GetTags http://localhost:5000/StackOverFlow/Tags 

    Query parameters:

     page int

     pageSize int

     sortBy string

     descanding bool

   - if parametrs not set, endpoint returns first 100 records 

  2. RefresTags http://localhost:5000/StackOverFlow/RefreshData 

  3. Register http://localhost:5000/App/Users/Register

   Query parameters:

     name string

     password string

     role string
  
  4. Login http://localhost:5000/App/Users/Login

   Query parameters:

     name string

     password string

Websockets:

1. Chat ws://localhost:5000/chat 

    Receiving endpoints: ReceiveMessage

     returns string

   Methods:

     1.SendMessage(string senderName, string message, string? receiverId) !Preview, personal message not working
     
     - if receiverId set to null or empty string, messaage will be sended to all users

2. Logs ws://localhost:5000/logs

    Receiving endpoints: ReceiveLog

     returns string

Jobs:

WebApi Hangfire dashbord http://localhost:5000/dashbord

ETL Hangfire dashbord http://localhost:5100/dashbord

Rabbit:

http://localhost:15672

Login: guest
Password: guest

Fronted:

http://localhost:3000
 

