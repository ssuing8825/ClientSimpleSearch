sqlcmd -S 10.0.0.4 -d Preclone_Client -U "Steve" -P "Steve" -s"	" -W -Q "SELECT abs(objrel.ClientId), objrel.TechnicalObjectKey, UPPER( REPLACE( objreltype.Description, ' ', '_')) as description FROM ObjectRelation objrel INNER JOIN lookup.ObjectRelationType objreltype ON objrel.RelationTypeCode = objreltype.RelationTypeCode Inner JOIN IndividualClient ic on objrel.ClientId = ic.ClientId order by abs(objrel.ClientId)  set nocount on" > MainRelations.csv