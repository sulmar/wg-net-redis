# Prezentacja Redis na 127 spotkaniu grupy WG.NET 


## Redis

### Instalacja

Uruchomienie Redis w dockerze
~~~
docker run --name wg-net-redis -d -p 6379:6379 redis
~~~

Uruchomienie trybu interaktywnego
~~~
docker exec -it wg-net-redis redis-cli
~~~

Uruchomienie kontenera
~~~ bash
docker start wg-net-redis 
~~~

Zatrzymanie kontenera
~~~ bash
docker stop wg-net-redis
~~~

Sprawdzenie czy Redis odpowiada
~~~
ping
~~~

Śledzenie Redisa
~~~
monitor
~~~

### Klucze

Dodanie wartości
~~~
SET current:user Marcin
~~~
Kolejne wywołanie nadpisze poprzednią wartość (update).

Jeśli chcemy tego uniknąć należy dodać atrybut NX
~~~
SET current:user Marcin NX
~~~

Pobranie wartości
~~~
GET current:user
~~~


Usunięcie klucza
~~~
DEL current:user
~~~


### TTL

Dodanie wartości na określony czas (TTL)
~~~
set vehicle1 ready ex 120
~~~

Ustawienie czasu życia klucza
~~~
expire vehicle1 60
~~~



Pobranie czasu, który pozostał do wygaśnienia klucza
~~~
ttl vehicle1
~~~

Pobranie wszystkich kluczy wg szablonu
~~~
keys *
~~~


 ### Inkrementacja
 
 Dodanie 1 do klucza
 ~~~
 incr points
 ~~~
 
 Dodanie określonej liczby do klucza
 ~~~
  incrby points 10
 ~~~
 
 Odjęcie 1 od klucza
 ~~~
 decr points
 ~~~
 

### Baza danych

Wybór bazy danych
~~~
select 1
~~~


###  Tablice asocjacyjne (HASH)

Jeśli chcesz zmodyfikować cały obiekt zwykły string wystarczy:

~~~ 
SET users:marcin { 'email': 'marcin.sulecki@gmail.com', speed: 120}
~~~

Natomiast w przypadku, gdy chcesz mieć dostęp do pojedynczych pól lepszym rozwiązaniem będą tablice asosjacyjne.

Dodanie
~~~
HSET users:marcin email marcin.sulecki@gmail.com
HSET users:marcin speed 120
~~~


Pobranie wybranego pola
~~~
HGET users:marcin email
~~~
 

Dodanie wielu wartości
~~~
HMSET users:marcin speed 120 email marcin.sulecki@gmail.com
~~~

Pobranie wszystich pól
~~~
HGETALL users:marcin
~~~

 

### Listy

Wstawianie elementu do listy (na początek)
~~~ 
lpush pages page1
lpush pages page2
lpush pages page3
~~~

Pobranie elementów z listy na podstawie zakresu
~~~
lrange pages 0 3
~~~


Usunięcie i pobranie pierwszego elementu z listy
~~~
lpop pages
~~~

Usunięcie i pobranie ostatniego elementu z listy
~~~
rpop pages
~~~

Dołączenie elementu do listy (na koniec)
~~~ 
lpush orders order1
lpush orders order2
lpush orders order3
~~~


Pobranie elementu z listy na podstawie indeksu
~~~
lindex orders 2
~~~


### Zbiory

Dodanie wartości do zbioru
~~~
sadd online user1
sadd online user2
sadd online user3
sadd offline user4
sadd offline user5
~~~

Pobranie elementów zbioru
~~~
smembers online
~~~

Usunięcie elementu ze zbioru
~~~
srem online user1
~~~

Przesunięcie elementu pomiędzy zbiorami
~~~
smove offline online user5
~~~

Suma zbiorów
~~~
sunion online offline
~~~

Część wspólna zbiorów
~~~
sinter online offline
~~~

Różnica zbiorów
~~~
sdiff
~~~

### Posortowane zbiory

Dodanie elementów
~~~
ZADD skills:marcin 100 csharp
ZADD skills:marcin 94 wpf-mvvm
ZADD skills:marcin 2 python
~~~

Pobranie elementów wg rankingu
~~~
ZRANGEBYSCORE skills:marcin 50 100
~~~

### Typy przestrzenne

Dodanie pozycji
~~~
geoadd locations 52.361389 19.115556 Vehicle1
geoadd locations 52.361389 19.115556 Vehicle2
geoadd locations 52.361389 19.115556 Vehicle3
geoadd locations 52.361389 19.115556 Vehicle4
~~~

Pobranie pozycji określonego klucza
~~~
geopos locations Vehicle2
~~~


Obliczenie dystansu pomiędzy dwoma pozycjami
~~~ 
geodist locations Vehicle1 Vehicle4 km
~~~


Wyszukanie pozycji w określonym promieniu
~~~
georadius locations 0 0 22000 km
~~~

### Czyszczenie 

Wyczyszczenie wszystkich kluczy ze wszystkich baz danych
~~~
flushall
~~~

Wyczyszczenie wszystkich kluczy z bieżącej bazy danych
~~~
flushdb
~~~


Wyczyszczenie wszystkich kluczy z określonej bazy danych
~~~
-n <database_number> flushdb
~~~


### Pub/Sub

Utworzenie subskrypcji
~~~
subscribe sensors:temp1
~~~

Wysłanie wiadomości
~~~
publish sensors:temp1 54.21
~~~

Usunięcie subskrypcji
~~~
UNSUBSCRIBE
~~~

Utworzenie subskrypcji ze wzorcem 
~~~
psubscribe sensors.temp*
~~~

## Redis i .NET Core

### Utworzenie klienta

Instalacja biblioteki

~~~ bash
dotnet add package StackExchange.Redis
~~~

Utworzenie połączenia

~~~ csharp
 ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
 IDatabase db = redis.GetDatabase();
~~~

#### String

~~~ csharp
 
 string key = "foo";

 db.StringSet(key, "Boo");

 string value = db.StringGet(key);
~~~


Inkrementacja

~~~ csharp
string key = "points";

 db.StringIncrement(key);

 db.StringIncrement(key);

 string value = db.StringGet(key);

 System.Console.WriteLine(value);

 db.StringDecrement(key);

~~~


### Biblioteka rozszerzająca
~~~ bash
StackExchange.Redis.Extensions
~~~
