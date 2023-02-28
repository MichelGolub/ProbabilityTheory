import { vuexOidcCreateStoreModule } from "vuex-oidc"

import { oidcSettings } from "@/config/oidc"

export default vuexOidcCreateStoreModule(oidcSettings, { isAuthenticatedBy: 'access_token' })