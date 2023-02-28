<template>
    <div v-show="!editing">
        <div class="valign-wrapper">
            <div class="col s8"><h4>{{ theme.title }}</h4></div>
            <div v-if="authorized" class="col s4">
                <button @click="askForRemoveTheme(theme.id)" class="waves-effect waves-light btn-small red darken-1 right">
                    <DeleteIcon/> {{ $t('theme.remove') }}
                </button>
                <button @click="toggleEditing" class="waves-effect waves-light btn-small right" style="margin-right: 4px">
                    <EditIcon/> {{ $t('theme.edit') }}
                </button>
            </div>
        </div>
    </div>

    <div v-show="editing">
        <ThemeForm :theme="theme" v-on:done="toggleEditing" />
    </div>

    <ThemeDeleteModal ref='themeDeleteModal'/>
</template>

<script>
import ThemeForm from '@/components/Theme/ThemeForm.vue'
import ThemeDeleteModal from '@/components/Theme/ThemeDeleteModal.vue'

import {  mapGetters } from 'vuex'

import DeleteIcon from 'icons/AlertCircle'
import EditIcon from 'icons/Pencil'

export default {
    name: 'themeTitleItem',
    components: {
        ThemeForm, ThemeDeleteModal,
        DeleteIcon, EditIcon
    },
    props: ['theme'],
    data() {
        return {
            editing: false
        }
    },
    computed: {
        ...mapGetters([
            'oidcUser'
        ]),
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) }
    },
    methods: {
        toggleEditing() {
            this.editing = !this.editing
        },
        askForRemoveTheme(id) {
            this.$refs.themeDeleteModal.open(id)
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "theme": {
            "edit": "edit",
            "remove": "delete"
        }
    },
    "ukr": {
        "theme": {
            "edit": "редагувати",
            "remove": "видалити"
        }
    }
}
</i18n>