SELECT G.Name
FROM Ganres AS G
WHERE NOT EXISTS(
	(SELECT FG.FilmId
	FROM FilmGanreRelationship AS FG INNER JOIN Films
	ON FG.FilmId = Films.Id
	WHERE Films.Year = FilmYear
	)EXCEPT(
	SELECT FG2.FilmId
	FROM FilmGanreRelationship AS FG2
	WHERE FG2.GanreId = G.Id
	)
);