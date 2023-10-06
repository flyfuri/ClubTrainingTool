# ClubTrainingTool

### What it is used for:
Database desktop application used to handle training of a Swiss Paragliding Aerobatic Club. The club organizes safe aerobatic training above a lake by providing the rescue boat (in the case someone uses his rescue parachute) and a van to drive up the to the take off. It is highly customized and would probably need substantial modification to be useful for another club or purpose. I put it public as a showcase of my privat work. It was a learning project for me to get some hands on C# WPF SQL.

### Functions:
- keeping track of flights and work pilots have done during training (at each run, one of the pilots has to drive the bus and another has to drive the rescue boat).
- keeping track of spending during training (ex. fuel). Receipts can be photographed using the webcam of the laptop/tablet.
- keeping track of money in the training wallet and the regular flow to the club bank account.
- keeping track of prepaid flights. Pilots could buy a virtual 10 flight card (called “Abo”) and make a photo of a generated QR-Code appearing of the screen.
- facilitate payments through TWINT (Swiss payment service like apple-Pay) or via e-banking app by generating corresponding QR-Codes. (added 2022). Pilots could then either scan that code with their TWINT- or e-banking app. Or just take a picture or it and scan it at home to make the payment.

[![Watch the video](https://img.youtube.com/vi/S8m4GdPU8RI/maxresdefault.jpg)](youtu.be/S8m4GdPU8RI)

### How it's done: 
Originaly written in 2016 using WinForms. For the Interaction with the database I chose to use dapper instead of relying on Entity Framework(). 2022 rewritten using WPF. It doesn't use any normaly used pattern like MVVM() and it rarely uses binding(*). This was done due to time restrictions for the project and specially to not break the already proven logic. Therefore even some WinForm methods which didn't exist in WPF where rebuilt with extention methods.

(*)I considered the Entity Framework, MVVM and bindings very useful, but only if someone plans to work in that field and use it more often. Therefor I spared this learning curves not only myself, but also others who migth have to maintain the software in the future.

### Know design flaws: 
see issues

### The following NuGetPackages where used: 
aforge 2.2.5 aforge.video 2.2.5 aforge.video.directshow 2.2.5 codecrete.swissqrbill.generator 2.5.3 csvhelper 27.2.0 dapper 2.0.123 net.codecrete.qrcodegenerator 1.6.1 qrcoder 1.4.2
