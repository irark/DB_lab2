SELECT DISTINCT Categories.Name
FROM Categories INNER JOIN
	(Films INNER JOIN
		(FilmGanreRelationship INNER JOIN Ganres
		ON FilmGanreRelationship.GanreId = Ganres.Id)
	ON Films.Id = FilmGanreRelationship.FilmId
	)
	ON Categories.Id = Films.CategoryId
WHERE(Ganres.Name = GanreName);