export class Ticket{
    public ticketId:string;
    public title:string;
    public description:string;
    public createdEmployeeId:string;
    public ticketTypeId:string;
    public statusId:string;
    public assignedEmployeeId:string;
    constructor(ticketId:string,title:string,description:string,createdEmployeeId:string,ticketTypeId:string,statusId:string,assignedEmployeeId:string){
        this.ticketId=ticketId;
        this.title=title;
        this.description=description;
        this.createdEmployeeId=createdEmployeeId;
        this.ticketTypeId=ticketTypeId;
        this.statusId=statusId;
        this.assignedEmployeeId=assignedEmployeeId;
    }
}