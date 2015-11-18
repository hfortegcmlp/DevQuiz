Assumptions:
- Application will parse by string and also trim each input: ' morning    ' == 'morning'
- Blank string will be treated as error: morning, 1,,2 will output eggs, error
- Requirement input of: morning 1, 2, 3, 3, 3 is a typo, should have a comma after morning
- Application will not handle multiple meals per dishtype - i.e, only one entree, only one dessert etc.