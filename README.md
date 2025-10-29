Projekt oparty na DDD z rozdzieleniem warstw (Persistence, CQRS — osobne repozytoria, SaveAsync poza domeną). Struktura została celowo rozbudowana, by pokazać dobre praktyki architektoniczne, mimo że przy tej skali byłaby nieopłacalna w realnym wdrożeniu.  

Model nie zawiera Value Objectów — ich zastosowanie byłoby zasadne pod kątem spójności i enkapsulacji, ale przy obecnym zakresie funkcjonalności ich implementacja byłaby nieproporcjonalnie pracochłonna.

Podgląd api w platformie Azure:

Fronted: 

http://4.210.65.98:3000/


Endpointy:

http://4.210.65.98:5000/StackOverFlow/Tags 

http://4.210.65.98:5000/StackOverFlow/Tags/RefreshData

http://4.210.65.98:5000/App/Users/Register

http://4.210.65.98:5000/App/Users/Login


GraphQl:
http://4.210.65.98:5000/graphql

Websokety:

ws://4.210.65.98:5000/chat 

ws://4.210.65.98:5000/logs


RabbitMq: 

http://4.210.65.98:15672/


Hangfire:

http://4.210.65.98:5000/dashbord

http://4.210.65.98:5100/dashbord


Applikacje można uruchomić poprzez docker conpose za pomocą polecenia: docker-compose up

__________________________________________________________________________________________________________________________

Backend:


Endpointy:

1. GetTags http://localhost:5000/StackOverFlow/Tags 

    Query:

     page int

     pageSize int

     sortBy string

     descanding bool

   - jeżeli nie ustawiono parametrów,zwracane jest pierwsze 100 rekordów, sortowanych po Id

  2. RefresTags http://localhost:5000/StackOverFlow/Tags/RefreshData 

  3. Register http://localhost:5000/App/Users/Register

   Query:

     name string

     password string

     role string
  
  4. Login http://localhost:5000/App/Users/Login

   Query:

     name string

     password string

Websokety:

1. Chat ws://localhost:5000/chat 

    Endpoiny do odbioru:
   ReceiveMessage, zwraca wiadomość string

   Metody:

     1.SendMessage(string senderName, string message, string? receiverId) !Nie zaimplementowano wiadomości do konkretnego uzytkownika
     
     - jeżeli nie podano receiver Id, wiadomość wysłana jest do wszystkich aktualnie zalogowanyvh użytkowników 

3. Logs ws://localhost:5000/logs

    Endpointy do odbioru:
     ReceiveLog, zwraca wiadomość string

Zadania hangfire:

WebApi Hangfire dashbord http://localhost:5000/dashbord

ETL Hangfire dashbord http://localhost:5100/dashbord

Rabbit:

http://localhost:15672

Login: guest
Hasło: guest

Fronted:

http://localhost:3000

GraphQl:
http://localhost/graphql/
 

