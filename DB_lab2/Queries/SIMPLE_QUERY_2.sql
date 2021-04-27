SELECT Films.Name
FROM Films
WHERE Films.Id IN
	(SELECT FilmGanreRelationship.FilmId
	 FROM FilmGanreRelationship INNER JOIN Ganres
	 ON Ganres.Id = FilmGanreRelationship.GanreId
	 WHERE Ganres.Name = GanreName AND Films.Year > FilmYear);
