insert into Employee values('E0001','Karan','Karan@gmail.com,','Karan@123',1);
insert into Employee values('E0002','Vishwesh','Vishwesh@17','Vishwesh@123er',0);
 
 
insert into [TICKET-TYPE] values('T0001','Software','software issues');
insert into [Status]values ('ST001','Pending','Correct quick','true');
 
insert into SLA values('S0001','T0001',8,12,'Service tag');
insert into Ticket values('T0001','Software issue','Windows error','E0001','T0001','ST001','E0002');
 
select* from Employee
select* from TICKET
select* from [TICKET-TYPE];
select * from SLA
select * from status
 
insert into TicketReplies values('R0001','T0001','E0001','E0002','The windows are not working',GETDATE())
select * from TicketReplies
 