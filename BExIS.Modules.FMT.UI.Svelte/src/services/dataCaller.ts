import { Api } from '@bexis2/bexis2-core-ui';


export const getFiles = async (menuitem: string) => {

    let url = "/FMT/BaseFileManagement/GetFiles/" + menuitem;
    
    const res = await Api.get(url);

    
       return res.data;
}