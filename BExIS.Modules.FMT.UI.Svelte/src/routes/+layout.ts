import { setApiConfig } from '@bexis2/bexis2-core-ui';

/** @type {import('./$types').PageLoad} */
export async function load({ fetch }) {
	if (import.meta.env.DEV) {
		  setApiConfig('http://localhost:44345', 'epetzold', '2021.B2.Go$On');
	}

	return {};
}

export const prerender = true;
export const ssr = false;
export const trailingSlash = 'always';
