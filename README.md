# Contact:
- **Mail**: *lytranvinh.work@gmail.com*
- **Mail**: *khiemhm04@gmail.com*

# How to run:
- Run database in docker:
    + **Note**: changle path volume.
    + After running
    ```
        docker compose up -d
    ```
- Init table db
    + **Note**: Install `dotnet ef global`
    + Create table **ApplicationDbContext**:
        ```
            dotnet ef database update --context ApplicationDbContext
        ```
    +  Create table **IdentityDbContext**:
        ```
            dotnet ef database update --context IdentityDbContext
        ```
    + Create table **QuanLySinhVienDbContext**:
        ```
            dotnet ef database update --context QuanLySinhVienDbContext
        ```
    + Create table **SessionDbContext**:
        ```
            dotnet ef database update --context SessionDbContext
        ```
    + Create table **ViewDbContext**:
        ```
            dotnet ef database update --context ViewDbContext
        ```
    + Run application:
        ```
            dotnet run
        ```