# COMPREHENSIVE STORED PROCEDURES vs ENTITY MODELS AUDIT REPORT
**Generated**: 2025-01-02  
**Project**: ODB_Cursova  
**Scope**: Full audit of 8 core entities with complete procedure and table schema verification

---

## EXECUTIVE SUMMARY

**Total Entities Audited**: 8  
**Total Discrepancies Found**: 2 CRITICAL  
**Entities with Issues**: 2 (ActionLog, Employee)

### Critical Issues Requiring Immediate Fix:
1. ✋ **ActionLog.CreatedBy** - Type mismatch between entity (Guid) and database (NVARCHAR(255))
2. ⚠️ **Employee.Salary** - Type mismatch between entity (float) and database (DECIMAL(18,2)) - Financial data precision loss

---

## DETAILED AUDIT BY ENTITY

---

### === ENTITY: ACTIONLOG ===

#### Database Table: ActionsLog
**File**: `DataBase/Tables/ActionsLog.sql`

| Column | SQL Type | Constraints |
|--------|----------|------------|
| Id | UNIQUEIDENTIFIER | NOT NULL, PRIMARY KEY |
| Description | NVARCHAR(MAX) | NOT NULL |
| Operation | NVARCHAR(255) | NOT NULL |
| CreatedAt | DATETIME2 | NOT NULL, DEFAULT (GETUTCDATE()) |
| CreatedBy | NVARCHAR(255) | NOT NULL |

#### Entity Model: ActionLog.cs
**File**: `WebAPI/Entities/ActionLog.cs`

| Property | C# Type | JSON Name |
|----------|---------|-----------|
| Id | Guid | N/A |
| Description | string | N/A |
| Operation | string | N/A |
| CreatedAt | DateTime | N/A |
| CreatedBy | Guid | N/A |

#### CRUD Stored Procedures

**sp_ActionLogs_GetAll**
- Returns: Id, Description, Operation, CreatedAt, CreatedBy (5 columns)
- Parameters: None

**sp_ActionLogs_GetById**
- Parameters: @Id UNIQUEIDENTIFIER
- Returns: All 5 columns for matching record

**sp_ActionLogs_Add**
```sql
@Id UNIQUEIDENTIFIER
@Description NVARCHAR(MAX)
@Operation NVARCHAR(255)
@CreatedAt DATETIME2 = NULL  (defaults to GETUTCDATE())
@CreatedBy NVARCHAR(255)
```

**sp_ActionLogs_Update**
```sql
@Id UNIQUEIDENTIFIER
@Description NVARCHAR(MAX)
@Operation NVARCHAR(255)
@CreatedBy NVARCHAR(255)
```

**sp_ActionLogs_Delete**
- Parameters: @Id UNIQUEIDENTIFIER

#### Issues Found

| Field | Entity Type | Database Type | Issue | Severity | Impact |
|-------|-------------|---|-------|----------|--------|
| CreatedBy | `Guid` | `NVARCHAR(255)` | **TYPE MISMATCH** | 🔴 CRITICAL | Dapper cannot map NVARCHAR to Guid - will throw InvalidCastException at runtime |

**Analysis**: The procedure expects a string value for @CreatedBy (NVARCHAR(255)), but the entity property is defined as Guid. When DbAccessService calls the procedure with entity properties, it will try to pass a Guid where a string is expected. This will cause immediate runtime failures when adding/updating ActionLog records.

**Recommendation**: Change ActionLog.CreatedBy from `Guid` to `string`

---

### === ENTITY: BILLINGNUMBER ===

#### Database Table: BillingNumbers
**File**: `DataBase/Tables/BillingNumbers.sql`

| Column | SQL Type | Constraints |
|--------|----------|------------|
| Id | UNIQUEIDENTIFIER | NOT NULL, PRIMARY KEY, DEFAULT (NEWID()) |
| AccountNumber | NVARCHAR(50) | NOT NULL, UNIQUE |
| Balance | DECIMAL(18, 2) | NOT NULL, DEFAULT (0) |
| Currency | NVARCHAR(10) | NOT NULL, DEFAULT ('UAH') |
| AccountType | INT | NOT NULL, DEFAULT (1) |
| Status | INT | NOT NULL, DEFAULT (1) |
| CreatedAt | DATETIME2 | NOT NULL, DEFAULT (GETUTCDATE()) |
| UpdatedAt | DATETIME2 | NOT NULL, DEFAULT (GETUTCDATE()) |
| CustomerId | UNIQUEIDENTIFIER | NOT NULL |

#### Entity Model: BillingNumber.cs
**File**: `WebAPI/Entities/BillingNumber.cs`

| Property | C# Type |
|----------|---------|
| Id | Guid |
| AccountNumber | string |
| Balance | decimal |
| Currency | string |
| AccountType | AccountType (enum → INT) |
| Status | AccountStatus (enum → INT) |
| CreatedAt | DateTime |
| UpdatedAt | DateTime |
| CustomerId | Guid |

#### CRUD Stored Procedures
- **sp_BillingNumbers_GetAll**: Returns 9 columns
- **sp_BillingNumbers_GetById**: @Id UNIQUEIDENTIFIER
- **sp_BillingNumbers_Add**: @Id, @AccountNumber, @Balance, @Currency, @AccountType, @Status, @CreatedAt, @UpdatedAt, @CustomerId
- **sp_BillingNumbers_Update**: All parameters except @CreatedAt (8 parameters)
- **sp_BillingNumbers_Delete**: @Id UNIQUEIDENTIFIER

#### Issues Found
✅ **NONE** - All type mappings are correct

---

### === ENTITY: BILLINGOPERATION ===

#### Database Table: BillingOperations
**File**: `DataBase/Tables/BillingOperations.sql`

| Column | SQL Type | Constraints |
|--------|----------|------------|
| Id | UNIQUEIDENTIFIER | NOT NULL, PRIMARY KEY |
| Amount | DECIMAL(18, 2) | NOT NULL |
| Currency | NVARCHAR(3) | NOT NULL |
| CreatedAt | DATETIME2 | NOT NULL |
| Description | NVARCHAR(255) | NULL |
| PaymentPurpose | INT | NOT NULL |
| CustomerId | UNIQUEIDENTIFIER | NULL |
| BillingNumberIdFrom | UNIQUEIDENTIFIER | NULL |
| BillingNumberIdTo | UNIQUEIDENTIFIER | NULL |
| CreditId | UNIQUEIDENTIFIER | NULL |

#### Entity Model: BillingOperation.cs
**File**: `WebAPI/Entities/BillingOperation.cs`

| Property | C# Type |
|----------|---------|
| Id | Guid |
| Amount | decimal |
| Currency | string |
| CreatedAt | DateTime |
| Description | string? (nullable) |
| PaymentPurpose | PaymentPurpose (enum → INT) |
| CustomerId | Guid? (nullable) |
| BillingNumberIdFrom | Guid? (nullable) |
| BillingNumberIdTo | Guid? (nullable) |
| CreditId | Guid? (nullable) |

#### CRUD Stored Procedures
- **sp_BillingOperations_GetAll**: Returns all 10 columns
- **sp_BillingOperations_GetById**: @Id UNIQUEIDENTIFIER
- **sp_BillingOperations_Add**: All 10 parameters with correct types
- **sp_BillingOperations_Update**: All 10 parameters
- **sp_BillingOperations_Delete**: @Id UNIQUEIDENTIFIER

#### Issues Found
✅ **NONE** - All type mappings are correct

---

### === ENTITY: BRANCH ===

#### Database Table: Branches
**File**: `DataBase/Tables/Branches.sql`

| Column | SQL Type | Constraints |
|--------|----------|------------|
| Id | UNIQUEIDENTIFIER | NOT NULL, PRIMARY KEY |
| Name | NVARCHAR(255) | NOT NULL |
| NumberOfEmployees | INT | NOT NULL |
| NumberOfBranch | INT | NOT NULL |
| Location | NVARCHAR(255) | NOT NULL |
| ContactEmail | NVARCHAR(255) | NOT NULL |
| ContactPhone | NVARCHAR(50) | NOT NULL |
| CreatedAt | DATETIME2 | NOT NULL, DEFAULT GETDATE() |

#### Entity Model: Branch.cs
**File**: `WebAPI/Entities/Branch.cs`

| Property | C# Type |
|----------|---------|
| Id | Guid |
| Name | string |
| NumberOfEmployees | int |
| NumberOfBranch | int |
| Location | string |
| ContactEmail | string |
| ContactPhone | string |
| CreatedAt | DateTime |

#### CRUD Stored Procedures
- **sp_Branches_GetAll**: Returns 8 columns
- **sp_Branches_GetById**: @Id UNIQUEIDENTIFIER
- **sp_Branches_Add**: All 8 parameters
- **sp_Branches_Update**: 7 parameters (excludes CreatedAt)
- **sp_Branches_Delete**: @Id UNIQUEIDENTIFIER

#### Issues Found
✅ **NONE** - All type mappings are correct

**Note**: Unlike other entities, Branch only has `CreatedAt` and no `UpdatedAt` field. This is intentional per the database schema and is correctly reflected in both entity and procedures.

---

### === ENTITY: CARD ===

#### Database Table: Cards
**File**: `DataBase/Tables/Cards.sql`

| Column | SQL Type | Constraints |
|--------|----------|------------|
| Id | UNIQUEIDENTIFIER | NOT NULL, PRIMARY KEY |
| CardNumber | NVARCHAR(16) | NOT NULL |
| Status | INT | NOT NULL |
| CardHolderName | NVARCHAR(255) | NOT NULL |
| LaunchDate | DATETIME2 | NOT NULL, DEFAULT (GETUTCDATE()) |
| ExpirationDate | DATETIME2 | NOT NULL |
| cvv | INT | NOT NULL |
| BillingNumberId | UNIQUEIDENTIFIER | NOT NULL |
| CustomerId | UNIQUEIDENTIFIER | NOT NULL |

#### Entity Model: Card.cs
**File**: `WebAPI/Entities/Card.cs`

| Property | C# Type |
|----------|---------|
| Id | Guid |
| CardNumber | string |
| Status | CardStatus (enum → INT) |
| CardHolderName | string |
| LaunchDate | DateTime |
| ExpirationDate | DateTime |
| cvv | int |
| BillingNumberId | Guid |
| CustomerId | Guid |

#### CRUD Stored Procedures
- **sp_Cards_GetAll**: Returns 9 columns
- **sp_Cards_GetById**: @Id UNIQUEIDENTIFIER
- **sp_Cards_Add**: All 9 parameters with correct types
- **sp_Cards_Update**: 8 parameters (excludes LaunchDate)
- **sp_Cards_Delete**: @Id UNIQUEIDENTIFIER

#### Issues Found
✅ **NONE** - All type mappings are correct

---

### === ENTITY: CREDIT ===

#### Database Table: Credits
**File**: `DataBase/Tables/Credits.sql`

| Column | SQL Type | Constraints |
|--------|----------|------------|
| Id | UNIQUEIDENTIFIER | NOT NULL, PRIMARY KEY |
| FullAmount | DECIMAL(18, 2) | NOT NULL |
| RemainingToPay | DECIMAL(18, 2) | NOT NULL |
| MonthlyPayment | DECIMAL(18, 2) | NOT NULL |
| DurationInMonths | INT | NOT NULL |
| Currency | NVARCHAR(3) | NOT NULL |
| CreatedAt | DATETIME2 | NOT NULL |
| NextPayment | DATETIME2 | NOT NULL |
| LastPayment | DATETIME2 | NULL |
| ClosedAt | DATETIME2 | NULL |
| IsClosed | BIT | NOT NULL, DEFAULT 0 |
| BillingNumberId | UNIQUEIDENTIFIER | NOT NULL |

#### Entity Model: Credit.cs
**File**: `WebAPI/Entities/Credit.cs`

| Property | C# Type |
|----------|---------|
| Id | Guid |
| FullAmount | decimal |
| RemainingToPay | decimal |
| MonthlyPayment | decimal |
| DurationInMonths | int |
| Currency | string |
| CreatedAt | DateTime |
| NextPayment | DateTime |
| LastPayment | DateTime? (nullable) |
| ClosedAt | DateTime? (nullable) |
| IsClosed | bool |
| BillingNumberId | Guid |

#### CRUD Stored Procedures
- **sp_Credits_GetAll**: Returns 12 columns
- **sp_Credits_GetById**: @Id UNIQUEIDENTIFIER
- **sp_Credits_Add**: All 12 parameters with correct types
- **sp_Credits_Update**: 11 parameters (excludes CreatedAt)
- **sp_Credits_Delete**: @Id UNIQUEIDENTIFIER

#### Issues Found
✅ **NONE** - All type mappings are correct

---

### === ENTITY: CUSTOMER ===

#### Database Table: Customers
**File**: `DataBase/Tables/Customers.sql`

| Column | SQL Type | Constraints |
|--------|----------|------------|
| Id | UNIQUEIDENTIFIER | NOT NULL, PRIMARY KEY, DEFAULT (NEWID()) |
| Name | NVARCHAR(MAX) | NOT NULL |
| Surname | NVARCHAR(MAX) | NOT NULL |
| Email | NVARCHAR(MAX) | NOT NULL |
| PasswordHash | NVARCHAR(MAX) | NOT NULL |
| CreatedAt | DATETIME2 | NOT NULL, DEFAULT (GETUTCDATE()) |

#### Entity Model: Customer.cs
**File**: `WebAPI/Entities/Customer.cs`

| Property | C# Type |
|----------|---------|
| Id | Guid |
| Name | string |
| Surname | string |
| Email | string |
| PasswordHash | string |
| CreatedAt | DateTime |

#### CRUD Stored Procedures
- **sp_Customers_GetAll**: Returns 6 columns
- **sp_Customers_GetById**: @Id UNIQUEIDENTIFIER
- **sp_Customers_Add**: All 6 parameters with correct types
- **sp_Customers_Update**: 5 parameters (excludes CreatedAt)
- **sp_Customers_Delete**: @Id UNIQUEIDENTIFIER

#### Issues Found
✅ **NONE** - All type mappings are correct

---

### === ENTITY: EMPLOYEE ===

**Note**: Employee inherits from Customer class

#### Database Table: Employees
**File**: `DataBase/Tables/Employees.sql`

| Column | SQL Type | Constraints |
|--------|----------|------------|
| Id | UNIQUEIDENTIFIER | NOT NULL, PRIMARY KEY, DEFAULT (NEWID()) |
| Name | NVARCHAR(MAX) | NOT NULL |
| Surname | NVARCHAR(MAX) | NOT NULL |
| Email | NVARCHAR(MAX) | NOT NULL |
| PasswordHash | NVARCHAR(MAX) | NOT NULL |
| CreatedAt | DATETIME2 | NOT NULL, DEFAULT (GETUTCDATE()) |
| Role | INT | NOT NULL |
| BranchId | UNIQUEIDENTIFIER | NOT NULL |
| Salary | DECIMAL(18, 2) | NOT NULL |
| HiredAt | DATETIME2 | NOT NULL |

#### Entity Model: Employee.cs (Inherits from Customer)
**File**: `WebAPI/Entities/Employee.cs`

**Inherited from Customer:**
| Property | C# Type |
|----------|---------|
| Id | Guid |
| Name | string |
| Surname | string |
| Email | string |
| PasswordHash | string |
| CreatedAt | DateTime |

**Employee-specific:**
| Property | C# Type |
|----------|---------|
| Role | EmployeeRole (enum → INT) |
| BranchId | Guid |
| Salary | **float** |
| HiredAt | DateTime |

#### CRUD Stored Procedures
- **sp_Employees_GetAll**: Returns 10 columns
- **sp_Employees_GetById**: @Id UNIQUEIDENTIFIER
- **sp_Employees_Add**: All 10 parameters with correct types
- **sp_Employees_Update**: 9 parameters (excludes CreatedAt)
- **sp_Employees_Delete**: @Id UNIQUEIDENTIFIER

#### Issues Found

| Field | Entity Type | Database Type | Issue | Severity | Impact |
|-------|-------------|---|-------|----------|--------|
| Salary | `float` | `DECIMAL(18, 2)` | **TYPE MISMATCH** | 🟠 HIGH | **Financial Data Precision Loss** |

**Analysis**: 
- **float** uses IEEE 754 binary representation (single precision or double precision)
- **DECIMAL(18,2)** uses base-10 representation with exactly 2 decimal places
- A salary of 2500.50 stored as float might become 2500.4999... or 2500.5000... leading to rounding errors
- In financial/banking systems, this precision loss is unacceptable
- The stored procedure expects DECIMAL(18,2), but the entity passes float

**Example of Problem**:
```
Employee salary in DB: 2500.50 (exactly 2 decimals)
Read as float: 2500.5000000001 or 2500.4999999999
Calculations accumulate errors
Display: "Employee earned $2500.50" but internally 2500.5000123456
```

**Recommendation**: Change Employee.Salary from `float` to `decimal`

---

## CROSS-REFERENCE: PARAMETER MAPPING VERIFICATION

### Mapping Flow: Entity → Dapper → Stored Procedure

The DbAccessService uses Dapper's parameter mapping, which:
1. Takes entity properties by reflection
2. Prefixes parameter names with `@`
3. Passes values to stored procedure

**Example for ActionLog.Add**:
```csharp
// Entity properties:
Id (Guid) → @Id (UNIQUEIDENTIFIER) ✓
Description (string) → @Description (NVARCHAR(MAX)) ✓
Operation (string) → @Operation (NVARCHAR(255)) ✓
CreatedAt (DateTime) → @CreatedAt (DATETIME2) ✓
CreatedBy (Guid) → @CreatedBy (NVARCHAR(255)) ✗ TYPE MISMATCH
```

### Verified Procedures Status
- ✅ BillingNumber: All procedures have matching parameters
- ✅ BillingOperation: All procedures have matching parameters
- ✅ Branch: All procedures have matching parameters
- ✅ Card: All procedures have matching parameters
- ✅ Credit: All procedures have matching parameters
- ✅ Customer: All procedures have matching parameters
- ⚠️ ActionLog: CreatedBy parameter type mismatch
- ⚠️ Employee: Salary property type mismatch

---

## RECOMMENDATIONS & ACTION ITEMS

### Priority 1 (CRITICAL - Do Immediately)
1. **Fix ActionLog.CreatedBy**: Change from `Guid` to `string`
   - File: `WebAPI/Entities/ActionLog.cs`
   - Current: `public Guid CreatedBy { get; set; }`
   - Should be: `public string CreatedBy { get; set; }`
   - Risk: Current code will throw InvalidCastException at runtime

### Priority 2 (HIGH - Financial Data Integrity)
2. **Fix Employee.Salary**: Change from `float` to `decimal`
   - File: `WebAPI/Entities/Employee.cs`
   - Current: `public float Salary { get; set; }`
   - Should be: `public decimal Salary { get; set; }`
   - Risk: Financial data precision loss over multiple transactions

### Priority 3 (ENHANCEMENT - Optional)
3. **Consider adding UpdatedAt to Branch**
   - Currently Branch only tracks CreatedAt
   - Other entities have UpdatedAt for audit trails
   - Not critical but recommended for consistency

---

## VERIFICATION CHECKLIST

- [x] All stored procedures examined (40 procedures across 8 entities)
- [x] All entity models reviewed
- [x] All database tables verified
- [x] Parameter type mappings checked
- [x] Enum conversions validated (INT ↔ enum)
- [x] Nullable field mappings confirmed
- [x] Return column sets verified
- [x] Foreign key constraints reviewed
- [x] Default value handling checked
- [x] CreatedAt/UpdatedAt fields audited

---

## AUDIT METHODOLOGY

1. **Source Examination**: Reviewed actual source files for all 8 entities
2. **Procedure Analysis**: Examined 40 stored procedures (5 per entity) for parameters
3. **Table Schema Review**: Verified column definitions against entities
4. **Type Mapping Verification**: Cross-referenced C# types to SQL types
5. **Dapper Behavior**: Considered how Dapper maps properties to parameters
6. **Financial Data Standards**: Applied banking industry best practices

---

## APPENDIX: SQL TYPE TO C# TYPE MAPPING REFERENCE

| SQL Type | C# Type | Notes |
|----------|---------|-------|
| UNIQUEIDENTIFIER | Guid | 1:1 mapping |
| NVARCHAR | string | 1:1 mapping |
| INT | int | 1:1 mapping |
| DECIMAL(18,2) | decimal | ✓ Use for financial data |
| DECIMAL(18,2) | float | ✗ Avoid for financial data |
| DATETIME2 | DateTime | 1:1 mapping |
| BIT | bool | 1:1 mapping |
| INT (with enum) | enum | Enum maps to INT |

---

**Report End**
