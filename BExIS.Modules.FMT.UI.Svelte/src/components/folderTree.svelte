
<script lang="ts">
	import { Facets } from '@bexis2/bexis2-core-ui';

	import { groups } from './data';

	let selected = {
		Mediums: [],
		Genres: [],
		Authors: []
	};
</script>

<Facets {groups} bind:selected showAll />
