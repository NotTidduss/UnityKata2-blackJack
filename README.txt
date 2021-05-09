Unity Kata #2 - Black Jack

Functionality checklist:
*) read 52 cards
*) card shuffle
*) betting system
	*) money variable
	*) selection of betting options
*) receive cards
	*) round 1: face down
	*) round 2: face up
*) HIT system (count management)
	*) functionality for a natural = ace + 10
	*) functionality for normal counting
	*) functionality for a bust = value > 21 = loss
*) STAND sytem
	*) end round, reveal winner

Flow:
1) hit start (start with fixed amount of chips = 100)
2) select bet 	-> betting state
3) card dealing -> dealing state
4) hit or stand -> playing state
5) reveal 		-> finish state

Goal for this kata:
*) make a natural happen
*) being able to actually win a round
