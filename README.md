Application run from docker:

 start command: docker-compose up

__________________________________________________________________________________________________________________________

Endpoints:

1. GetTags baseUrl/StackOverFlow/Tags 

    Query parameters:

     page int

     pageSize int

     sortBy string

     descanding bool

   - if parametrs not set, endpoint returns first 100 records 

   2. RefresTags baseUrl/StackOverFlow/RefreshData 

Websockets:

1. Chat wss://localhost:port/chat 

    Receiving endpoints: ReceiveMessage

     returns string

   Methods:

     1.SendMessage(string senderName, string message, string? receiverId) !Preview, personal message not working
     
     - if receiverId set to null or empty string, messaage will be sended to all users

2. Logs wss://localhost:port/logs

    Receiving endpoints: ReceiveLog

     returns string

Jobs:

Hangfire dashbord baseUrl/dashbord
 

