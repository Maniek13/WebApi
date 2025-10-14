Application run from docker:

 start command: docker-compose up

__________________________________________________________________________________________________________________________

Endpoints:

1. GetTags https://localhostL5000/StackOverFlow/Tags 

    Query parameters:

     page int

     pageSize int

     sortBy string

     descanding bool

   - if parametrs not set, endpoint returns first 100 records 

  2. RefresTags https://localhostL5000/StackOverFlow/RefreshData 

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

Hangfire dashbord https://localhostL5000/dashbord

Fronted:
https://localhostL:3000
 

