USE [LowCostFlights]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchHistory](
	[DepartureAirport] [nvarchar](5) NOT NULL,
	[ArrivalAirport] [nvarchar](5) NOT NULL,
	[DepartureDate] [datetime] NOT NULL,
	[ReturnDate] [datetime] NOT NULL,
	[Currency] [nvarchar](5) NOT NULL,
	[Passengers] [int] NOT NULL,
	[JsonResult] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SearchHistory] PRIMARY KEY CLUSTERED 
(
	[DepartureAirport] ASC,
	[ArrivalAirport] ASC,
	[DepartureDate] ASC,
	[ReturnDate] ASC,
	[Currency] ASC,
	[Passengers] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO