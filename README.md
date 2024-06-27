# TNS-Test-API (.NET 8)

โครงสร้างโปรเจค ประบกอบด้วย

1. การเชื่อมต่อฐานข้อมูล PostgreSQL
ใช้ Entity Framework Core (EF Core) เพื่อเชื่อมต่อกับฐานข้อมูล PostgreSQL ได้โดยตรง โดยต้องติดตั้ง NuGet Package Npgsql.EntityFrameworkCore.PostgreSQL เพื่อให้ .NET Core สามารถเข้าถึงฐานข้อมูล PostgreSQL ได้

2. การสร้าง Model สำหรับ Entity
กำหนดโมเดล (model) สำหรับแต่ละ Entity ในฐานข้อมูล Department และ User

3. สร้าง DbContext สำหรับ Entity Framework Core เพื่อเชื่อมต่อกับฐานข้อมูล PostgreSQL

4. การสร้าง Controller สำหรับ API Endpoints เพื่อจัดการกับข้อมูลในฐานข้อมูลได้