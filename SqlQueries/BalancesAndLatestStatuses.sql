-------------------------------------------------------------------------------------------------------------------------
-----------Query assumes uniqueness on Status.EntityId + Status.Date - i.e, only one status per day per entity-----------
-----------Query assumes that it should replicate exactly the outpu from the dev quiz------------------------------------
-------------------------------------------------------------------------------------------------------------------------
SELECT E.EntityName, B.BalanceDate AS "Date", B.Balance, S.Status
FROM Balances B
CROSS APPLY (
	SELECT MAX (StatusDate) as MostRecentStatusDate
	FROM status
	WHERE StatusDate <= B.BalanceDate
		AND EntityId = B.EntityId
) StatusDate
INNER JOIN Entity E ON E.EntityId = B.EntityId
LEFT OUTER JOIN Status S ON S.StatusDate = StatusDate.MostRecentStatusDate AND S.EntityId =B.EntityId
-----------Query assumes that it should replicate exactly the output from the dev quiz - hence the additional order by---
ORDER BY E.EntityName DESC, B.BalanceDate DESC