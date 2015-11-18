-------------------------------------------------------------------------------------------------------------------------
-----------Query assumes uniqueness on Status.EntityId + Status.Date - i.e, only one status per day per entity-----------
-------------------------------------------------------------------------------------------------------------------------
SELECT E.EntityName, LastStatus.Status, LastStatus.StatusDate AS "As Of", AverageBalance.[Avg Balance]
FROM Entity E
INNER JOIN (
	SELECT Entity.EntityId, avg(balance) AS 'Avg Balance'
	FROM Balances
	INNER JOIN Entity ON Entity.EntityId = Balances.EntityId
	GROUP BY Entity.EntityId
) AS AverageBalance ON E.EntityId = AverageBalance.EntityId
INNER JOIN (
	SELECT S.entityId, S.StatusDate, S.status
	FROM Status S
	INNER JOIN (
		SELECT entityId, max(StatusDate) AS MaxDate
		FROM Status
		GROUP BY entityId
	) MaxDate ON S.entityId = MaxDate.entityId and S.StatusDate = MaxDate.MaxDate
) LastStatus ON E.entityId = LastStatus.entityId
ORDER BY E.EntityName DESC