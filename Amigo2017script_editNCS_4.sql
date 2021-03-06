USE [Nissan_Amigo]
GO
/****** Object:  Table [dbo].[CUSTOMER_MASTER]    Script Date: 5/15/2020 11:36:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CUSTOMER_MASTER](
	[COMPANY_NO_BOX] [char](10) NOT NULL,
	[TRANSACTION_TYPE] [int] NOT NULL,
	[EFFECTIVE_DATE] [date] NOT NULL,
	[UPDATE_CONTENT] [int] NULL,
	[COMPANY_NAME] [nvarchar](80) NULL,
	[COMPANY_NAME_READING] [nvarchar](160) NULL,
	[ESTIMATED_SUBMISSION_DATE] [date] NULL,
	[CONTRACT_DATE] [date] NULL,
	[COMPLETION_NOTE_SENDING_DATE] [date] NULL,
	[CONTRACTOR_DEPARTMENT_IN_CHARGE] [nvarchar](80) NULL,
	[CONTRACTOR_CONTACT_NAME] [nvarchar](60) NULL,
	[CONTRACTOR_CONTACT_NAME_READING] [nvarchar](40) NULL,
	[CONTRACTOR_POSTAL_CODE] [char](8) NULL,
	[CONTRACTOR_ADDRESS] [nvarchar](100) NULL,
	[CONTRACTOR_ADDRESS-2] [nvarchar](100) NULL,
	[CONTRACTOR_MAIL_ADDRESS] [nvarchar](50) NULL,
	[CONTRACTOR_PHONE_NUMBER] [nvarchar](14) NULL,
	[BILL_SUPPLIER_NAME] [nvarchar](80) NULL,
	[BILL_SUPPLIER_NAME_READING] [nvarchar](160) NULL,
	[BILL_COMPANY_NAME] [nvarchar](80) NULL,
	[BILL_DEPARTMENT_IN_CHARGE] [nvarchar](80) NULL,
	[BILL_CONTACT_NAME] [nvarchar](60) NULL,
	[BILL_CONTACT_NAME_READING] [nvarchar](40) NULL,
	[BILL_POSTAL_CODE] [char](8) NULL,
	[BILL_ADDRESS] [nvarchar](100) NULL,
	[BILL_ADDRESS-2] [nvarchar](100) NULL,
	[BILL_MAIL_ADDRESS] [nvarchar](50) NULL,
	[BILL_PHONE_NUMBER] [nvarchar](14) NULL,
	[BILL_METHOD] [char](4) NULL,
	[NCS_CUSTOMER_CODE] [nchar](6) NOT NULL,
	[BILL_BANK_ACCOUNT_NAME-1] [nchar](48) NULL,
	[BILL_BANK_ACCOUNT_NAME-2] [nchar](48) NULL,
	[BILL_BANK_ACCOUNT_NAME-3] [nchar](48) NULL,
	[BILL_BANK_ACCOUNT_NAME-4] [nchar](48) NULL,
	[BILL_BANK_ACCOUNT_NUMBER-1] [char](10) NULL,
	[BILL_BANK_ACCOUNT_NUMBER-2] [char](10) NULL,
	[BILL_BANK_ACCOUNT_NUMBER-3] [char](10) NULL,
	[BILL_BANK_ACCOUNT_NUMBER-4] [char](10) NULL,
	[BILL_BILLING_INTERVAL] [int] NULL,
	[BILL_DEPOSIT_RULES] [int] NULL,
	[BILL_TRANSFER_FEE] [money] NULL,
	[BILL_EXPENSES] [money] NULL,
	[PLAN_SERVER] [int] NULL,
	[PLAN_SERVER_LIGHT] [int] NULL,
	[PLAN_BROWSER_AUTO] [int] NULL,
	[PLAN_BROWSER] [int] NULL,
	[PLAN_INITIAL_COST] [money] NULL,
	[PLAN_MONTHLY_COST] [money] NULL,
	[PLAN_AMIGO_CAI] [int] NULL,
	[PLAN_AMIGO_BIZ] [int] NULL,
	[PLAN_ADD_BOX_SERVER] [int] NULL,
	[PLAN_ADD_BOX_BROWSER] [int] NULL,
	[PLAN_OP_FLAT] [int] NULL,
	[PLAN_OP_SSL] [int] NULL,
	[PLAN_OP_BASIC_SERVICE] [int] NULL,
	[PLAN_OP_ADD_SERVICE] [int] NULL,
	[PLAN_OP_SOCIOS] [int] NULL,
	[PREVIOUS_COMPANY_NAME] [nvarchar](80) NULL,
	[NML_CODE_NISSAN] [char](4) NULL,
	[NML_CODE_NS] [char](4) NULL,
	[NML_CODE_JATCO] [char](4) NULL,
	[NML_CODE_AK] [char](4) NULL,
	[NML_CODE_NK] [char](4) NULL,
	[NML_CODE_OTHER] [char](4) NULL,
	[UPDATE_DATE] [date] NULL,
	[UPDATER_CODE] [char](6) NULL,
 CONSTRAINT [PK_CUSTOMER_MASTER] PRIMARY KEY CLUSTERED 
(
	[COMPANY_NO_BOX] ASC,
	[TRANSACTION_TYPE] ASC,
	[EFFECTIVE_DATE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INVOICE_INFO]    Script Date: 5/15/2020 11:36:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INVOICE_INFO](
	[TRANSACTION_TYPE] [nchar](2) NOT NULL,
	[COMPANY_NO_BOX] [nchar](10) NOT NULL,
	[YEAR_MONTH] [nchar](7) NOT NULL,
	[INVOICE_DATE] [date] NOT NULL,
	[NCS_CUSTOMER_CODE] [char](6) NULL,
	[BILL_SUPPLIER_NAME] [nvarchar](80) NULL,
	[BILL_SUPPLIER_NAME_READING] [nvarchar](160) NULL,
	[BILL_COMPANY_NAME] [nvarchar](80) NULL,
	[BILL_DEPARTMENT_IN_CHARGE] [nvarchar](80) NULL,
	[BILL_CONTACT_NAME] [nvarchar](60) NULL,
	[BILL_HONORIFIC] [nvarchar](4) NULL,
	[BILL_POSTAL_CODE] [char](8) NULL,
	[BILL_ADDRESS] [nvarchar](100) NULL,
	[BILL_ADDRESS-2] [nvarchar](100) NULL,
	[PLAN_SERVER] [int] NULL,
	[PLAN_SERVER_LIGHT] [int] NULL,
	[PLAN_BROWSER_AUTO] [int] NULL,
	[PLAN_BROWSER] [int] NULL,
	[PLAN_AMIGO_CAI] [int] NULL,
	[PLAN_AMIGO_BIZ] [int] NULL,
	[PLAN_ADD_BOX_SERVER] [int] NULL,
	[PLAN_ADD_BOX_BROWSER] [int] NULL,
	[PLAN_OP_FLAT] [int] NULL,
	[PLAN_OP_SSL] [int] NULL,
	[PLAN_OP_BASIC_SERVICE] [int] NULL,
	[PLAN_OP_ADD_SERVICE] [int] NULL,
	[PLAN_OP_SOCIOS] [int] NULL,
	[BILL_DEPOSIT_RULES] [nchar](1) NULL,
	[BILL_AMOUNT] [money] NULL,
	[BILL_TAX] [money] NULL,
	[BILL_PRICE] [money] NULL,
	[BILL_TRANSFER_FEE] [money] NULL,
	[BILL_EXPENSES] [money] NULL,
	[BILL_PAYMENT_DATE] [date] NULL,
	[STATUS_PRINT] [date] NULL,
	[STATUS_MEMO] [nvarchar](50) NULL,
	[STATUS_SEND] [date] NULL,
	[STATUS_MAIL_DATE] [char](4) NULL,
	[STATUS_ACC_RECEIVABLE_DATE] [date] NULL,
	[STATUS_INVOCE_COPY_DATE] [date] NULL,
	[STATUS_ACTUAL_MDB_UPDATE] [date] NULL,
	[STATUS_ACTUAL_DEPOSIT_YYMM] [char](4) NULL,
	[STATUS_ACTUAL_DEPOSIT_DATE] [date] NULL,
	[STATUS_CASH_FORECAST_MM] [char](2) NULL,
	[STATUS_PLAN_DEPOSIT_YYMM] [char](4) NULL,
	[STATUS_PAY_NOTICE_DATE] [date] NULL,
	[TYPE_OF_ALLOCATION] [int] NULL,
	[ALLOCATION_TOTAL_AMOUNT] [money] NULL,
	[ALLOCATED_COMPLETION_DATE] [datetime2](7) NULL,
	[DUNNING_COUNT] [int] NULL,
	[DUNNING_DATE_1] [date] NULL,
	[ANSWER_DATE_1] [date] NULL,
	[PAYMENT_DUE_DATE_1] [date] NULL,
	[ANSWER_MEMO_1] [nvarchar](50) NULL,
 CONSTRAINT [PK_INVOICE_INFO] PRIMARY KEY CLUSTERED 
(
	[TRANSACTION_TYPE] ASC,
	[COMPANY_NO_BOX] ASC,
	[YEAR_MONTH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RECEIPT_DETAILS]    Script Date: 5/15/2020 11:36:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RECEIPT_DETAILS](
	[SEQ_NO] [int] IDENTITY(1,1) NOT NULL,
	[DATA_CLASS] [int] NOT NULL,
	[RECORD_CLASS] [nchar](4) NOT NULL,
	[TRANSACTION_DATE] [datetime2](7) NOT NULL,
	[TRANSACTION_CONTACT_NAME] [nvarchar](48) NOT NULL,
	[TRANSACTION_BANKS_NAME] [nvarchar](50) NOT NULL,
	[TRANSACTION_BRANCH_NAME] [nvarchar](23) NOT NULL,
	[TRANSACTION_ACCOUNT_NO_CLASS] [nchar](8) NOT NULL,
	[TRANSACTION_ACCOUNT_TYPE] [nchar](10) NOT NULL,
	[TRANSACTION_ACCOUNT_NO] [nchar](12) NOT NULL,
	[RESEND_INDICATION] [nchar](1) NULL,
	[TRANSACTION_NAME] [nchar](8) NOT NULL,
	[TRANSACTION_NO] [nchar](4) NOT NULL,
	[TRANSACTION_DETAIL_CLASS] [nchar](4) NULL,
	[HANDLING_DATE] [datetime2](7) NOT NULL,
	[DEPOSIT_DATE] [datetime2](7) NOT NULL,
	[DEPOSIT_AMOUNT] [money] NOT NULL,
	[CHECK_CLASS] [nchar](8) NULL,
	[CUSTOMER_NAME] [nvarchar](48) NOT NULL,
	[COLLECTION_NO_SHEETS] [int] NULL,
	[COLLECTION_NO] [nchar](6) NULL,
	[CUSTOMER_NO] [nvarchar](20) NULL,
	[BANK_NAME] [nvarchar](15) NOT NULL,
	[BRANCH_NAME] [nvarchar](15) NOT NULL,
	[TRANSFER_MESSAGE] [nvarchar](20) NULL,
	[NOTE] [nvarchar](50) NULL,
	[NUMBER] [int] NULL,
	[TRANSACTION_FILE_NAME] [nvarchar](50) NOT NULL,
	[RUN_DATE_TIME] [datetime2](7) NOT NULL,
	[RUN_RESULT] [int] NOT NULL,
	[DATA_MOVEMENT_INFORMATION] [nchar](1) NULL,
	[PAYMENT_DAY] [datetime2](7) NULL,
	[TYPE_OF_ALLOCATION] [int] NULL,
	[ALLOCATED_QUANTITY] [int] NULL,
	[ALLOCATED_MONEY] [money] NULL,
	[ALLOCATED_COMPLETION_DATE] [datetime2](7) NULL,
 CONSTRAINT [PK_RECEIPT_DETAILS] PRIMARY KEY CLUSTERED 
(
	[SEQ_NO] ASC,
	[HANDLING_DATE] ASC,
	[DEPOSIT_AMOUNT] ASC,
	[CUSTOMER_NAME] ASC,
	[BANK_NAME] ASC,
	[BRANCH_NAME] ASC,
	[TRANSACTION_FILE_NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RECEIPT_DETAILS_NON_AMIGO]    Script Date: 5/15/2020 11:36:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RECEIPT_DETAILS_NON_AMIGO](
	[SEQ_NO] [int] IDENTITY(1,1) NOT NULL,
	[DATA_CLASS] [int] NOT NULL,
	[RECORD_CLASS] [nchar](4) NOT NULL,
	[TRANSACTION_DATE] [datetime2](7) NOT NULL,
	[TRANSACTION_CONTACT_NAME] [nvarchar](48) NOT NULL,
	[TRANSACTION_BANKS_NAME] [nvarchar](50) NOT NULL,
	[TRANSACTION_BRANCH_NAME] [nvarchar](23) NOT NULL,
	[TRANSACTION_ACCOUNT_NO_CLASS] [nchar](8) NOT NULL,
	[TRANSACTION_ACCOUNT_TYPE] [nchar](10) NOT NULL,
	[TRANSACTION_ACCOUNT_NO] [nchar](12) NOT NULL,
	[RESEND_INDICATION] [nchar](1) NULL,
	[TRANSACTION_NAME] [nchar](8) NOT NULL,
	[TRANSACTION_NO] [nchar](4) NOT NULL,
	[TRANSACTION_DETAIL_CLASS] [nchar](4) NULL,
	[HANDLING_DATE] [datetime2](7) NOT NULL,
	[DEPOSIT_DATE] [datetime2](7) NOT NULL,
	[DEPOSIT_AMOUNT] [money] NOT NULL,
	[CHECK_CLASS] [nchar](8) NULL,
	[CUSTOMER_NAME] [nvarchar](48) NOT NULL,
	[COLLECTION_NO_SHEETS] [int] NULL,
	[COLLECTION_NO] [nchar](6) NULL,
	[CUSTOMER_NO] [nvarchar](20) NULL,
	[BANK_NAME] [nvarchar](15) NOT NULL,
	[BRANCH_NAME] [nvarchar](15) NOT NULL,
	[TRANSFER_MESSAGE] [nvarchar](20) NULL,
	[NOTE] [nvarchar](50) NULL,
	[NUMBER] [int] NULL,
	[TRANSACTION_FILE_NAME] [nvarchar](50) NOT NULL,
	[RUN_DATE_TIME] [datetime2](7) NOT NULL,
	[RUN_RESULT] [int] NOT NULL,
	[DATA_MOVEMENT_INFORMATION] [nchar](1) NULL,
	[PAYMENT_DAY] [datetime2](7) NULL,
	[TYPE_OF_ALLOCATION] [int] NULL,
	[ALLOCATED_QUANTITY] [int] NULL,
	[ALLOCATED_MONEY] [money] NULL,
	[ALLOCATED_COMPLETION_DATE] [datetime2](7) NULL,
 CONSTRAINT [PK_RECEIPT_DETAILS_NON_AMIGO] PRIMARY KEY CLUSTERED 
(
	[SEQ_NO] ASC,
	[HANDLING_DATE] ASC,
	[DEPOSIT_AMOUNT] ASC,
	[CUSTOMER_NAME] ASC,
	[BANK_NAME] ASC,
	[BRANCH_NAME] ASC,
	[TRANSACTION_FILE_NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVE_INFO]    Script Date: 5/15/2020 11:36:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVE_INFO](
	[SEQ_NO] [int] NOT NULL,
	[BILLING_CODE] [nchar](17) NOT NULL,
	[PAYMENT_DAY] [datetime2](7) NULL,
	[TYPE_OF_ALLOCATION] [int] NULL,
	[RESERVE_AMOUNT] [money] NULL,
 CONSTRAINT [PK_RESERVE_INFO] PRIMARY KEY CLUSTERED 
(
	[SEQ_NO] ASC,
	[BILLING_CODE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USER_MASTER]    Script Date: 5/15/2020 11:36:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER_MASTER](
	[ID] [nchar](6) NOT NULL,
	[PASS] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_USER_MASTER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
