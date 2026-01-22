
export class Employee{
    public employeeId:string;
    public employeeName:string;
    public email:string;
    public password:string;
    public role: string;

    constructor(employeeId:string,employeeName:string,email:string,password:string,role:string){
        this.employeeId=employeeId;
        this.employeeName=employeeName;
        this.email=email;
        this.password= password;
        this.role=role;
    }
   

}