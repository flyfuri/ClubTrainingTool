# ClubTrainingTool

### What it is used for:
Database desktop application used to handle training of a Swiss Paragliding Aerobatic Club. The club organizes safe aerobatic training above a lake by providing a rescue boat (in case someone uses his rescue parachute) and a van to drive up to the takeoff site. It is highly customized and would probably need substantial modification to be of any use for another club or purpose. I put it public as a showcase of my private work. It was a learning project for me to get some hands-on C# WPF SQL.

### Functions:
- keeping track of flights and work pilots have done during training (at each run, one of the pilots has to drive the bus, and one has to steer the rescue boat).
- keeping track of spending during training (ex. fuel). A picture of the receipt can be taken using the webcam of the laptop/tablet the application is running on.
- keeping track of money in the training wallet and the regular flow to the club bank account.
- keeping track of prepaid flights. Pilots could buy a virtual 10 flight card (called “Abo”) and optionally take a photo of a generated QR-Code appearing on the screen.
- facilitate payments through TWINT (Swiss payment service like Apple Pay) or via an e-banking app by generating corresponding QR codes. (added 2022). Pilots could either scan that code with their TWINT- or e-banking app or take a picture of it and scan it at home to make the payment.

[![Watch the video](https://img.youtube.com/vi/S8m4GdPU8RI/maxresdefault.jpg)](https://www.youtube.com/embed/S8m4GdPU8RI)

### How it is done: 
Originally written in 2016 using WinForms. For the Interaction with the database, I chose to use Dapper instead of relying on Entity Framework(). 2022 rewritten using WPF. It doesn't use any normally used pattern like MVVM(), and it rarely uses binding. This was done due to time restrictions for the project and especially to avoid breaking the already proven logic. Therefore, even some WinForm methods that didn't exist in WPF were rebuilt with extension methods.
I considered the Entity Framework, MVVM, and bindings very useful, but only if someone plans to work in that field and use it more often. Therefore, I spared those learning curves not only for myself but also for others who might have to maintain the software in the future.

### Know design flaws: 
see issues

### The following NuGetPackages were used: 
aforge 2.2.5 aforge.video 2.2.5 aforge.video.directshow 2.2.5 codecrete.swissqrbill.generator 2.5.3 csvhelper 27.2.0 dapper 2.0.123 net.codecrete.qrcodegenerator 1.6.1 qrcoder 1.4.2

###How to test it with Visual Studio (I used 2019 and I haven't tested on another machine than mine):

- clone repo
- checkout branch UseThisForDemo
- copy the whole folder "TESTDATA_TrainingsTool" to your C drive.(This contains a set with random data).
- open the project in Visual Studio
- If you skip the following 3 parts, it might still work. Only the database backup function won't work and give you an error when creating a new Training:
- in Visual Studio: open Server Explorer
- "connect to database" (choose "database file" and "Windows authentication" and browse to the file "TrainingsRapport.mdf" in "C:\TESTData...")
- in Visual Studio open SQL Server Object Explorer and rename the database to "TRAININGSRAPPORT"
- start the application from Visual Studio
