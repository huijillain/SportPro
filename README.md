Apply the programming skills with ASP.NET Core MVC by developing a website called SportsPro Technical Support. 

This website is designed for the technical support department of a hypothetical software company that develops 
software for sports leagues. The purpose of the website is to track technical support service calls (referred 
to as incidents) in a database that also stores information about the company’s customers, software products, 
and technicians.

Website:
The SportsPro Technical Support website consists of web pages that support two types of users. First, it
lets administrators manage the products, technicians, customers, incidents, and product registrations
that are in the database. Second, it lets technicians update incidents that have been assigned to them.

Database:
The database for the SportsPro website stores the data that’s needed to track technical support
incidents. For the completed website, the database should include tables for storing data about the
company’s products, technicians, customers, incidents, and registrations. The start project contains Data
migration files which you can use to generate the required Database.
The Incidents table contains one row for each technical support incident. Each row in the Incidents table
is related to one row in the Customers table, which contains data about the company’s customers; one
row in the Products table, which contains data about the company’s products; and one row in the
Technicians table, which contains data about the company’s technical support staff. In addition, each
row in the Customers table is related to one row in the Countries table, which stores a list of available
countries.
The Registrations table, on the other hand, is a linking table that creates a many-to-many relationship
between the Customers and Products tables. As a result, each row relates one customer to one product.
This allows one customer to register many products, and it allows one product to be registered by many
customers.
