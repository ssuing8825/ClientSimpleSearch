sqlcmd -S .\SQL2012 -d Preclone_Client -E -s"	" -W -Q "SELECT objrel.TechnicalObjectKey, abs(objrel.ClientId), UPPER( REPLACE( objreltype.Description, ' ', '_')) as description FROM ObjectRelation objrel INNER JOIN lookup.ObjectRelationType objreltype ON objrel.RelationTypeCode = objreltype.RelationTypeCode Inner JOIN IndividualClient ic on objrel.ClientId = ic.ClientId" > C:\Users\Steven.Suing\Desktop\batch_importer_20\MainRelations.csv