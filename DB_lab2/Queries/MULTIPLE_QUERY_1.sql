SELECT DISTINCT A.Name
FROM Actors AS A
WHERE NOT EXISTS(
	(SELECT FilmActorRelationship.FilmId
	FROM FilmActorRelationship INNER JOIN Actors
	ON FilmActorRelationship.ActorId = Actors.Id
	WHERE(Actors.Name = ActorName)
	)EXCEPT(
	SELECT FA.FilmId
	FROM FilmActorRelationship AS FA
	WHERE(FA.ActorId = A.Id)
	)
);