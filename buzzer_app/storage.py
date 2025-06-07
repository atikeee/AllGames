buzzer_entries = []
name_locks = {}
codenames_words = []
codenames_colors = []
hint_log = []
current_game = {
    'words': [],
    'colors': [],
    'revealed': set(),  # store indices of revealed words
    'team': 'red',
}
