# Train_Ticket_Reservation_System_Web-Backend

** Language : C#, ASP .NET

** Introduction **

In response to the growing demand for a streamlined and efficient Train Ticket Reservation System in Sri Lanka, we have developed a comprehensive solution that caters to both back- office operations and traveler needs. Our system is built on a robust client-server architecture, featuring a web application for back-office functions and a mobile application for travelers. This end-to-end Ticket Reservation system offers a range of functionalities designed to enhance the user experience and ensure seamless management of train bookings.

**

* Web Application ** 
  
1)  User Management : - In our web application, we have established a two-tiered user management system. Users can be categorized into two distinct roles: Backoffice and Travel Agent. This segregation of roles ensures that only Backoffice users have access to certain critical functions within the system, maintaining data security and integrity.

2)  Traveler Management :- Traveler profiles can be easily created, updated, or deleted within the system, with the National Identification Card (NIC) serving as the primary key. Traveler accounts can be activated and deactivated, granting them control over their participation in the reservation process.

3)  Ticket Booking Management :- Our system simplifies the management of ticket bookings, offering features such as creating new reservations (with a limitation of a reservation date within 30 days from the booking date and a maximum of 4 reservations per reference ID). Additionally, reservations can be updated or canceled, provided that changes or cancellations are requested at least 5 days before the reservation date.

4)  Train Management :- Efficient train management is at the heart of our system. This functionality includes the creation of new trains, each complete with schedules. Notably, reservations can only be made once atrain is both active and published. Furthermore, train schedules can be updated, but trains with existing reservations cannot be canceled to prevent disruptions in travelers' plans.

5)  User Interface :- Our user interfaces are designed with elegance in mind, utilizing Bootstrap to provide a visually appealing and user-friendly experience.

** Back Officer (Backoffice) Features **

1. Back Officers can log in to the system using their email and password. The system checks
their email, password, and role.
2. Back Officers can view a list of all scheduled trains.
3. Back Officers can manage train schedules, including adding new trains, updating train details, and deleting trains.
4. Back Officers can manage user accounts, enabling them to deactivate and reactivate accounts.
5. Back Officers can view all reservations.
   
• Travel Agent Features

1. Travel Agents can register and log in to the application.
2. Travel Agents can register travelers in the application.
3. Travel Agents can book train tickets, with reservation dates within 30 days from the booking date, and a maximum of 4 reservations per reference ID.
4. Travel Agents can edit reservations, allowing changes at least 5 days before the reservation date.
5. Travel Agents can cancel reservations, permitting cancellations at least 5 days before the reservation date.
