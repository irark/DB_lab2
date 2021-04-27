SELECT DISTINCT Films.Name, Films.Year
FROM Films
WHERE Films.Id NOT IN
	(SELECT FilmGanreRelationship.FilmId
	 FROM FilmGanreRelationship INNER JOIN Ganres
	 ON Ganres.Id = FilmGanreRelationship.GanreId
	 WHERE Ganres.Name = GanreName);