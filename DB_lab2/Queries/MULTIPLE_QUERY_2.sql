SELECT F.Name
FROM Films AS F
WHERE NOT EXISTS(
	(SELECT FG.GanreId
	FROM FilmGanreRelationship AS FG
	WHERE FG.FilmId = F.Id
	) EXCEPT(
	SELECT FG2.GanreId
	FROM FilmGanreRelationship AS FG2 INNER JOIN Films
	ON FG2.FilmId = Films.Id
	WHERE Films.Name = FilmName
	)
);