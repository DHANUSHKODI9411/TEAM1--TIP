export class TicketReplies {
  public replyId: string;
  public ticketId: string;
  public createdEmployeeId: string;    
  public assignedEmployeeId?: string;   
  public replyText: string;
  public repliedDate: Date;

  constructor(
    replyId: string = "",
    ticketId: string = "",
    createdEmployeeId: string = "",
    assignedEmployeeId: string = "",
    replyText: string = "",
    repliedDate: Date = new Date()
  ) {
    this.replyId = replyId;
    this.ticketId = ticketId;
    this.createdEmployeeId = createdEmployeeId;
    this.assignedEmployeeId = assignedEmployeeId;
    this.replyText = replyText;
    this.repliedDate = repliedDate;
  }
}
