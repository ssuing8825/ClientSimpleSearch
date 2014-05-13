/****** Script for SelectTopNRows command from SSMS  ******/
SELECT count(*) as objrel FROM [PreClone_Client].[dbo].ObjectRelation

SELECT count(*) as relatedclient FROM [PreClone_Client].[dbo].RelatedClient

SELECT count(*) as externalPolicy FROM [PreClone_Client].[dbo].ExternalPolicy
SELECT count(*) as BatchReconciliation FROM [PreClone_Client].[dbo].BatchReconciliation
SELECT count(*) as Reconcilliation FROM [PreClone_Client].[dbo].Reconciliation




SELECT Top 100 *  FROM [PreClone_Client].[lookup].ObjectRelationType

SELECT Top 100 *  FROM [PreClone_Client].[lookup].ObjectSystem

SELECT TOP 1000 * FROM [PreClone_Client].[lookup].[ClientRelationType]

SELECT Top 100 * FROM [PreClone_Client].[dbo].ExternalPolicy
Select Distinct ObjectSystemId from [PreClone_Client].[dbo].ExternalPolicy
SELECT Top 100 * FROM [PreClone_Client].[lookup].ObjectSystem	

SELECT TOP 1000 * FROM [PreClone_Client].[lookup].ObjectType

SELECT Top 100 *  FROM [PreClone_Client].[dbo].IndividualClient

SELECT Top 100 *  FROM [PreClone_Client].[dbo].ObjectRelation
SELECT Top 100 *  FROM [PreClone_Client].[dbo].RelatedClient

SELECT Top 100 *  FROM [PreClone_Client].[dbo].ObjectRelation where TechnicalObjectKey = 'TNMIWRMF'
SELECT Top 100 *  FROM [PreClone_Client].[dbo].ObjectRelation where ObjectTypeCode = 'BACT'


Select top 3000 '{"method" : "POST","to" : "/node","body" : {
"Name" : "' + FirstName + '  ' + LastName + '",
"ClientId" : "' + cast(IndividualClient.ClientId as varchar) + '",
"ClientKey" : "' + Client.ClientKey + '"
"Type" : "CLIENT"
 }},' as fm, *
From IndividualClient inner join Client on Client.ClientId = IndividualClient.ClientId
where FirstName is not null and LastName is not null

Select ClientId
from Client

{"method" : "POST","to" : "/node","body" : {"age" : 1,"name" : "steve" },  "id" : 2

--Create the systems first because we will have all these things also have a relationship to the system
SELECT     Distinct lookup.ObjectSystem.Description
FROM            ObjectRelation INNER JOIN
                         lookup.ObjectSystem ON ObjectRelation.ObjectSystemId = lookup.ObjectSystem.ObjectSystemId
	
-- We don't need to create this since this will be the type of relationship.	
--Then the object Type
SELECT         lookup.ObjectType.Description, Count(*)
FROM            ObjectRelation INNER JOIN
                         lookup.ObjectType ON ObjectRelation.ObjectTypeCode = lookup.ObjectType.ObjectTypeCode
Group by lookup.ObjectType.Description

					 
--Then the relationship Type
SELECT         lookup.ObjectRelationType.Description, Count(*)
FROM            ObjectRelation INNER JOIN
                         lookup.ObjectRelationType ON ObjectRelation.RelationTypeCode = lookup.ObjectRelationType.RelationTypeCode
Group by lookup.ObjectRelationType.Description


SELECT distinct         objRel.ObjectTypeCode , ot.Description, Count(*)
FROM            ObjectRelation objRel INNER JOIN
                         lookup.ObjectType ot ON objRel.ObjectTypeCode = ot.ObjectTypeCode
Group by objRel.ObjectTypeCode , ot.Description
Order by Count(*) desc



SELECT   top 1000     objRel.TechnicalObjectKey, lookup.ObjectSystem.Description as systemType, lookup.ObjectType.Description AS objectType, rt.Description as relationship, 
                         objRel.ShowableObjectKey, objRel.ClientId
FROM            ObjectRelation objRel INNER JOIN
                         lookup.ObjectRelationType rt ON objRel.RelationTypeCode = rt.RelationTypeCode INNER JOIN
                         lookup.ObjectSystem ON objRel.ObjectSystemId = lookup.ObjectSystem.ObjectSystemId INNER JOIN
                         lookup.ObjectType ON objRel.ObjectTypeCode = lookup.ObjectType.ObjectTypeCode



//Program would run the queries and send in the batches. No need to do this one at a time.





SELECT   Top 2000     rc.RelatedClientId, rc.ClientId,rc.ReferenceTypeCode, crt.Description, crrt.Description crtDescription
FROM            RelatedClient AS rc INNER JOIN
                         lookup.ClientRelationType AS crt ON rc.RelationTypeCode = crt.RelationTypeCode AND rc.RelationTypeCode = crt.RelationTypeCode INNER JOIN
                         lookup.ClientRelationType as crrt ON rc.ReferenceTypeCode = crrt.RelationTypeCode
Where rc.ReferenceTypeCode  not in ('AKA', 'FKA', 'CKN','LKD')




MATCH (from {ClientKey: "000713TS8UI106187GCX"}),(to {ClientKey: "00052KX8NT204273GCXI"}) create from-[:MOTHER_OF]->to;







-- Create the individual Clients
-- Create the addressess
-- Create the Misc Objects from the Obj Relate
	-- Link the object to something previously created
		-- Link Address
-- Create Relationships between Clients using Clientrelationship
-- Create Recon relationships


exec dbo.GetClients