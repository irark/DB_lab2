SELECT DISTINCT Films.Name
FROM Films
WHERE Films.Id IN
	(SELECT FilmActorRelationship.FilmId
	 FROM FilmActorRelationship INNER JOIN Actors
	 ON Actors.Id = FilmActorRelationship.ActorId
	 WHERE Actors.Name = ActorName);
