<template>
    <nav>
        <div class="container">
            <div class="nav-wrapper">
                <router-link :to="{ name: 'home' }" class="brand-logo">{{ $t('projectName') }}</router-link>
                <ul id="nav-mobile" class="right hide-on-med-and-down">
                    <li :class="{ active: ['page', 'question', 'chapters'].includes($route.name) }">
                        <router-link :to="{ name: 'chapters' }">
                            {{ $t('chapters') }}
                        </router-link>
                    </li>
                    <li :class="{ active: ['term', 'terms'].includes($route.name) }">
                        <router-link :to="{ name: 'terms' }">
                            {{ $t('terms') }}
                        </router-link>
                    </li>
                    <li><LanguageSwitcher/></li>
                    <li><AuthInfo/></li>
                </ul>
            </div>
        </div>
    </nav> 
</template>


<script>
import LanguageSwitcher from '@/components/common/LanguageSwitcher.vue'
import AuthInfo from '@/components/common/AuthInfo.vue'

export default {
    name: 'NavbarMenu',
    components: {
        LanguageSwitcher,
        AuthInfo
    }
}
</script>


<i18n>
{
    "eng": {
        "chapters": "Chapters",
        "terms": "Terms"
    },
    "ukr": {
        "chapters": "Глави",
        "terms": "Терміни"
    }
}
</i18n>