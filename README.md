# README #

1.What is SDManager?

Sales department manager is an asp.net core application intendent for sales department :) It supports 3 types of users:
-Administrator  - this user manage user accounts. He could create them, assign role and name to them and delete them.
-Manager - At present this user has only access to every action, in the future I am going to give him righ tools to managing the department.
-Sales representative - This user could add new clients and their orders. He has access to all orders, but can modify only this one, which he create (Resource Based Requirements).

I develop this app for educational purposes and as a path of self-development in such technologies as asp.net core MVC 6, C#, SQL, JavaScript, jQuery, Bootstrap, Razor and learning english
which current level does not satisfied me enough.

I try to apply SOLID rules and project patterns (e.g. DI, Singleton).

Now the app is developing mainly in back-end, because this is the most important for me. In the future i am going to change the default layout and design of views.

2.Steps to start using app:
1)Apply migrations (dotnet ef database update in CMD).
2)When you use app for the first time, the app seed roles and administrator account, which you have to log in to create manager and sales representative accounts:
email: pawel.walnik@admin.com
password: Admin1.
3)Once you are logged in your admin account you are able to create manager ans SR accounts. After doing this log into SR account and discover the app - it is not complicated yet ;)
