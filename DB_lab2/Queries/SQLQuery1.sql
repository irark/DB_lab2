SELECT DISTINCT(Actors.Name)
FROM Actors
WHERE 1 < (SELECT COUNT(DISTINCT(Films.CategoryId))
	FROM Films INNER JOIN FilmActorRelationship
	ON Films.Id = FilmActorRelationship.FilmId
	WHERE FilmActorRelationship.ActorId = Actors.Id
	);