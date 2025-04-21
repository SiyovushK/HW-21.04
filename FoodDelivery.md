# Платформа доставки еды

## Описание проекта

Разработка RESTful API для платформы доставки еды с использованием C#, Entity Framework Core и PostgreSQL.

## Основные сущности

### 1. Рестораны (Restaurants)

| Поле           | Тип     | Описание                 |
| -------------- | ------- | ------------------------ |
| Id             | int     | Уникальный идентификатор |
| Name           | string  | Название ресторана       |
| Address        | string  | Адрес ресторана          |
| Rating         | decimal | Рейтинг (1-5)            |
| WorkingHours   | string  | Часы работы              |
| Description    | string  | Описание ресторана       |
| ContactPhone   | string  | Контактный телефон       |
| IsActive       | bool    | Статус активности        |
| MinOrderAmount | decimal | Минимальная сумма заказа |
| DeliveryPrice  | decimal | Стоимость доставки       |

### 2. Меню (Menu)

| Поле            | Тип     | Описание                  |
| --------------- | ------- | ------------------------- |
| Id              | int     | Уникальный идентификатор  |
| RestaurantId    | int     | ID ресторана              |
| Name            | string  | Название блюда            |
| Description     | string  | Описание блюда            |
| Price           | decimal | Цена                      |
| Category        | string  | Категория блюда           |
| IsAvailable     | bool    | Доступность               |
| PreparationTime | int     | Время приготовления (мин) |
| Weight          | int     | Вес (граммы)              |
| PhotoUrl        | string  | URL фотографии            |

### 3. Пользователи (Users)

| Поле             | Тип      | Описание                    |
| ---------------- | -------- | --------------------------- |
| Id               | int      | Уникальный идентификатор    |
| Name             | string   | Имя пользователя            |
| Email            | string   | Email                       |
| Phone            | string   | Телефон                     |
| Password         | string   | Хеш пароля                  |
| Address          | string   | Адрес доставки              |
| RegistrationDate | DateTime | Дата регистрации            |
| Role             | enum     | Роль (Client/Courier/Admin) |

### 4. Заказы (Orders)

| Поле            | Тип       | Описание                 |
| --------------- | --------- | ------------------------ |
| Id              | int       | Уникальный идентификатор |
| UserId          | int       | ID пользователя          |
| RestaurantId    | int       | ID ресторана             |
| CourierId       | int       | ID курьера               |
| OrderStatus     | enum      | Статус заказа            |
| CreatedAt       | DateTime  | Время создания           |
| DeliveredAt     | DateTime? | Время доставки           |
| TotalAmount     | decimal   | Общая сумма              |
| DeliveryAddress | string    | Адрес доставки           |
| PaymentMethod   | enum      | Способ оплаты            |
| PaymentStatus   | enum      | Статус оплаты            |

### 5. Детали заказа (OrderDetails)

| Поле                | Тип     | Описание                 |
| ------------------- | ------- | ------------------------ |
| Id                  | int     | Уникальный идентификатор |
| OrderId             | int     | ID заказа                |
| MenuItemId          | int     | ID позиции меню          |
| Quantity            | int     | Количество               |
| Price               | decimal | Цена за единицу          |
| SpecialInstructions | string  | Особые пожелания         |

### 6. Курьеры (Couriers)

| Поле            | Тип     | Описание                 |
| --------------- | ------- | ------------------------ |
| Id              | int     | Уникальный идентификатор |
| UserId          | int     | ID пользователя          |
| Status          | enum    | Статус (Active/Inactive) |
| CurrentLocation | string  | Текущая локация          |
| Rating          | decimal | Рейтинг (1-5)            |
| TransportType   | enum    | Тип транспорта           |

## Реализация сущностей

```csharp
public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal Rating { get; set; }
    public string WorkingHours { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public bool IsActive { get; set; }
    public decimal MinOrderAmount { get; set; }
    public decimal DeliveryPrice { get; set; }
}

public class Menu
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; set; }
    public int PreparationTime { get; set; }
    public int Weight { get; set; }
    public string PhotoUrl { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public UserRole Role { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int CourierId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}

public class OrderDetail
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string SpecialInstructions { get; set; }
}

public class Courier
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public CourierStatus Status { get; set; }
    public string CurrentLocation { get; set; }
    public decimal Rating { get; set; }
    public TransportType TransportType { get; set; }
}

public enum UserRole
{
    Client,
    Courier,
    Admin
}

public enum OrderStatus
{
    Created,
    Confirmed,
    InProgress,
    ReadyForDelivery,
    OnDelivery,
    Delivered,
    Cancelled
}

public enum PaymentMethod
{
    Cash,
    Card,
    OnlinePayment
}

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed,
    Refunded
}

public enum CourierStatus
{
    Active,
    Inactive,
    OnDelivery,
    OnBreak
}

public enum TransportType
{
    Bicycle,
    Motorcycle,
    Car,
    OnFoot
}
```

## API Endpoints

### Рестораны

```
GET    /api/restaurants              # Получение списка ресторанов с фильтрацией
GET    /api/restaurants/{id}         # Получение ресторана по ID
POST   /api/restaurants             # Создание нового ресторана
PUT    /api/restaurants/{id}        # Обновление ресторана
DELETE /api/restaurants/{id}        # Удаление ресторана
```

### Меню

```
GET    /api/menu                    # Получение всех меню
GET    /api/menu/{id}              # Получение меню по ID
POST   /api/menu                   # Создание новой меню
PUT    /api/menu/{id}             # Обновление меню
DELETE /api/menu/{id}             # Удаление меню
```

### Заказы

```
GET    /api/orders                  # Получение списка заказов
GET    /api/orders/{id}            # Получение заказа по ID
POST   /api/orders                 # Создание нового заказа
PUT    /api/orders/{id}           # Обновление заказа
DELETE /api/orders/{id}           # Удаление заказа
```

### Пользователи

```
GET    /api/users                   # Получение списка пользователей
GET    /api/users/{id}             # Получение пользователя по ID
POST   /api/users                  # Создание нового пользователя
PUT    /api/users/{id}            # Обновление пользователя
DELETE /api/users/{id}            # Удаление пользователя
```

### Курьеры

```
GET    /api/couriers               # Получение списка курьеров
GET    /api/couriers/{id}         # Получение курьера по ID
POST   /api/couriers              # Создание нового курьера
PUT    /api/couriers/{id}        # Обновление курьера
DELETE /api/couriers/{id}        # Удаление курьера
```

### Детали заказа

```
GET    /api/order-details                # Получение списка всех деталей заказов
GET    /api/order-details/{id}          # Получение деталей заказа по ID
GET    /api/order-details/order/{id}    # Получение всех деталей для конкретного заказа
POST   /api/order-details               # Создание новой детали заказа
PUT    /api/order-details/{id}          # Обновление детали заказа
DELETE /api/order-details/{id}          # Удаление детали заказа
```

### LINQ Задачи

```
1. GET /api/restaurants/active
    $ Получить список активных ресторанов, отсортированных по рейтингу

2. GET /api/menu/available
    $ Получить доступные блюда, где цена меньше 1000

3. GET /api/orders/by-status
    $ Посчитать количество заказов в каждом статусе

4. GET /api/menu/by-category
    $ Посчитать среднюю цену блюд в каждой категории

5. GET /api/users/with-orders
    $ Получить пользователей и количество их заказов
    $ Join Users и Orders, Count заказов для каждого пользователя

6. GET /api/orders/by-courier
    $ Получить заказы, выполненные конкретным курьером
    $ Join Orders и Couriers заказов для каждого курьера

7. GET /api/orders/total-today
    $ Получить общую сумму заказов за сегодня

8. GET /api/couriers/top-rated
    $ Получить топ-5 курьеров с наивысшим рейтингом

9. GET /api/orders/expensive
    $ Найти заказы с суммой выше средней

10. GET /api/menu/popular-category
    $ Найти категорию с наибольшим количеством блюд
    
11. GET /api/analytics/restaurants/top             
    $ Получить топ-10 ресторанов количеству заказов за последний месяц
    $ Join ресторанов и заказов, группировка по ресторану

12. GET /api/analytics/restaurants/revenue        
    $ Рассчитать выручку каждого ресторана по дням за последнюю неделю
    $ Группировка заказов по ресторану и дате

13. GET /api/analytics/restaurants/dishes/popular 
    $ Найти 3 самых популярных блюда для каждого ресторана
    $ Join заказов и деталей заказа, группировка по блюдам

14. GET /api/analytics/orders/peak-hours          
    $ Определить часы пик по количеству заказов
    $ Группировка заказов по часам, подсчет количества

15. GET /api/analytics/orders/average-check       
    $ Рассчитать средний чек заказов по районам города
    $ Группировка заказов по району доставки

16. GET /api/analytics/orders/delivery-time       
    $ Найти среднее время доставки для каждого района
    $ Вычисление разницы между временем заказа и доставки

17. GET /api/analytics/users/preferences          
    $ Определить любимые категории блюд каждого пользователя
    $ Join заказов, деталей и меню, группировка по пользователю

18. GET /api/analytics/users/retention            
    $ Найти пользователей, сделавших более 5 заказов за месяц
    $ Группировка заказов по пользователю с условием

19. GET /api/analytics/couriers/performance       
    $ Оценить работу курьеров: среднее время доставки и рейтинг
    $ Join заказов и курьеров, подсчет средних значений

20. GET /api/analytics/couriers/earnings          
    $ Рассчитать заработок курьеров за последний месяц
    $ Группировка заказов по курьеру, подсчет сумм
