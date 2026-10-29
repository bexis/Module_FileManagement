<script lang="ts">
import { onMount } from 'svelte';
import type { SvelteComponent } from 'svelte';

import {
  Page,
  Table,
  pageContentLayoutType
} from '@bexis2/bexis2-core-ui';
import * as dataCaller from '../../services/eventCaller';
import { writable } from 'svelte/store';

import type { TableConfig } from '@bexis2/bexis2-core-ui';
import { goto } from '$app/navigation';
import tableOptions from '../../components/tableOptions.svelte';
	import Fa from 'svelte-fa';
	import { faPlus } from '@fortawesome/free-solid-svg-icons';


onMount(async () => {
  const data = await dataCaller.getEvents();
  tableStore.set(sortEventsByIdDescending(data));
});


</script>

<Page title="Files"
	note=""
	contentLayoutType={pageContentLayoutType.center}>

  		<div class="grid grid-cols-2 gap-5 my-4 pb-1 border-b border-primary-500">
			<div class="h3 h-9">
				Create new Event
			</div>
			<div class="text-right">
			
         
			
			</div>
		</div>


  <div class="table table-compact w-full">
    <Table config={table}  on:action={e => handleTableAction(e)}/>
  </div>
</Page>