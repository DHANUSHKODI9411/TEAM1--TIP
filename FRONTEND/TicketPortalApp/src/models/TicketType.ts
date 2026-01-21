export class TicketType {
  public ticketTypeId: string;
  public ticketTypeName: string;
  public description: string;

  constructor(ticketTypeId: string = "", ticketTypeName: string = "", description: string = "") {
    this.ticketTypeId = ticketTypeId;
    this.ticketTypeName = ticketTypeName;
    this.description = description;
  }
}