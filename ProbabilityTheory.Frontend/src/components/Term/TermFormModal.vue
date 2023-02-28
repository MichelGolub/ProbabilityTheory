<template>
    <VForm @submit="update" :initial-values="term" :validation-schema="schema">
        <div class="row">
            <div class="input-field col s6">
                <label for="title" class="active">{{ $t('title') }}</label>
                <VField name="title" />
                <ErrorMessage name="title" />
            </div>
        </div>
        <div class="row">
            <div class="input-field">
                <label for="definition" class="active">{{ $t('definition') }}</label>
                <VField as="textarea" name="definition" />
                <ErrorMessage name="definition" />
            </div>
        </div>
        <div class="modal-footer">
            <button class="waves-effect waves-light btn-flat" type="submit">
                <ConfirmIcon/> {{ $t('button.confirm' )}}
            </button>
            <button class="waves-effect waves-light btn-flat" type="reset"
            @click="$emit('done')">
                <CancelIcon/> {{ $t('button.cancel' )}}
            </button>
        </div>
    </VForm>
</template>


<script>
import { mapActions } from 'vuex'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { object, required, string } from 'yup'

import ConfirmIcon from 'icons/Check'
import CancelIcon from 'icons/CloseCircle'

export default {
    name: "TermForm",
    props: ['term'],
    emits: ['done'],
    components: {
        "VForm": Form,
        "VField": Field,
        ErrorMessage,
        ConfirmIcon, CancelIcon
    },
    data() {
        const schema = object({
            title: string()
                .required()
                .max(50, this.$t('error.max', { num: 50})),
            definition: string()
                .required()
                .max(1000, this.$t('error.max', {num: 1000}))
        })

        return {
            schema
        }
    },
    computed: {
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) }
    },
    methods: {
        ...mapActions([
            'updateTerm'
        ]),
        update(values, { resetForm }) {
            let payload = {}
            payload['languageId'] = this.languageId
            payload['termId'] = this.term.id
            payload['title'] = values.title
            payload['definition'] = values.definition

            this.updateTerm(payload)
            .then(() => {
                this.$emit('done')
                resetForm()
            })
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "title": "title",
        "definition": "definition",
        "error": {
            "max": "This field must be not greater than {num} characters",
            "required": "This is a required field"
        }
    },
    "ukr": {
        "title": "назва",
        "definition": "визначення",
        "error": {
            "max": "Це поле не повинне бути більшим за {num} символів",
            "required": "Це необхідне поле"
        }
    }
}
</i18n>