export interface IEmployee {
    idNumber: string;
    firstName: string;
    lastName: string;
    role: string;
    managerId?: string;
    managerName?: string;
}


export interface IEmployeeDto {
    idNumber: string | null;
    firstName: string | null;
    lastName: string | null;
    role: string | null;
    managerId: string | null;
}