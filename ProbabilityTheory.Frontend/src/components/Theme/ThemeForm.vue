<template>
    <VForm @submit="update" :initial-values="theme" :validation-schema="schema">
        <div class="col s6">
            <div class="input-field">
                <label for="title" class="active">{{ $t('theme.title') }}</label>
                <VField name="title" />
                <ErrorMessage name="title" />
            </div>
        </div>
        <div class="right">
            <button class="waves-effect waves-light btn-small" style="margin-right: 4px" type="submit">
                <ConfirmIcon/> {{ $t('button.confirm' )}}
            </button>
            <button class="waves-effect waves-light btn-small grey darken-1" type="reset"
            @click="$emit('done')">
                <CancelIcon/> {{ $t('button.cancel' )}}
            </button>
        </div>
    </VForm>
</template>


<script>
import { Form, Field, ErrorMessage } from 'vee-validate'
import { object, string, required } from 'yup'

import { mapActions } from 'vuex'

import ConfirmIcon from 'icons/Check'
import CancelIcon from 'icons/CloseCircle'

export default {
    name: 'ThemeForm',
    props: ['theme'],
    emits: ['done'],
    data() {
        const schema = object({
            title: string().required()
        })

        return {
            schema    
        }
    },
    components: {
        VForm: Form,
        VField: Field,
        ErrorMessage,
        ConfirmIcon, CancelIcon
    },
    computed: {
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) }
    },
    methods: {
        ...mapActions([
            'updateTheme'
        ]),
        update(values, { resetForm }) {
            let payload = {}
            payload['languageId'] = this.languageId
            payload['themeId'] = this.theme.id
            payload['title'] = values.title

            this.updateTheme(payload)
            .then((resp) => {
                this.$emit('done')
                resetForm()
            })
        },
    }
}
</script>


<i18n>
{
    "eng": {
        "theme": {
            "title": "title"
        }
    },
    "ukr": {
        "theme": {
            "title": "назва"
        }
    }
}
</i18n>