# <div align="center"> Contact </div>
- **Mail**: *lytranvinh.work@gmail.com*
- **Mail**: *khiemhm04@gmail.com*

# <div align="center"> TODO </div>
- Input data with file
- Auth ...  
- ....

# <div align="center"> How to run  </div>
- Run database in docker:
    + **Note**: changle path volume.
    + After running
    ```
        docker compose up -d
    ```
- Init table db
    + **Note**: Install `dotnet ef global`
    ```bash
      dotnet tool install --global dotnet-ef

    ```
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
    + Run application (creaete admin and db init static):
        ```
            dotnet run
        ```
    + End Application.
- Run sql template
    + New query `QuanLySinhVienDbContext` path file `/sql/QuanLySinhVienDb.sql`.
    + New query `IdentityDbContext` path file `/sql/IdentityDb.sql`.
  
- Run application 
```
    dotnet run
```

# <div align="center">[DEMO](https://youtu.be/7JjGT8xnrl4) </div>

# <div align="center">Contribute</div>
<a href="https://github.com/Youknow2509/web_qlsv/graphs/contributors">
<img height="50" src="https://contrib.rocks/image?repo=Youknow2509/web_qlsv" alt="Ultralytics open-source contributors"></a>

