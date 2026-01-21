export class Status{
  public statusId: string;
  public statusName: string;
  public description: string;
  public isActive: boolean;
  constructor(statusId: string, statusName: string, description: string, isActive: boolean ){
    this.statusId = statusId;
    this.statusName = statusName;
    this.description = description;
    this.isActive = isActive;
  }
}
