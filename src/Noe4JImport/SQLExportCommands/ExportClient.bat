sqlcmd -S 10.0.0.4 -d Preclone_Client -U "Steve" -P "Steve" -s"	" -W -Q "Select  ABS(ic.ClientId) , FirstName + '  ' + LastName as name ,'CLIENT' as Label, Client.ClientKey From IndividualClient ic inner join Client on Client.ClientId = ic.ClientId where FirstName is not null and LastName is not null order by ABS(ic.ClientId) set nocount on" > ClientNodes.csv