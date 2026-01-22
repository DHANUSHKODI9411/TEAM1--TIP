export class SLA {
    public slAid: string;
    public ticketTypeId: string;
    public ticketTypeName: string;
    public description: string;
    public responseTime:number;
    public resolutionTime:number;
constructor(
    slAid: string,
    ticketTypeId: string,
    ticketTypeName: string,
    description: string,
    responseTime:number,
    resolutionTime:number){
        this.slAid = slAid;
        this.ticketTypeId = ticketTypeId;
        this.ticketTypeName = ticketTypeName;
        this.description = description;
        this.responseTime = responseTime;
        this.resolutionTime = resolutionTime;   
    }
}