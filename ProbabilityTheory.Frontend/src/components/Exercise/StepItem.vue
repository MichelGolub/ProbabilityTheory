<template>
    <div class="card-panel">
        <div class="row">
            <b>{{ $t('title', { num: number}) }}</b>
            <div v-if="authorized" v-show="!editing" class="right">
                <button @click="editing = true" class="btn-small" style="margin-right: 4px">
                    <EditIcon style=" cursor: pointer" /> {{ $t('edit') }}
                </button>
                <button  @click="removeStep" class="waves-effect waves-light btn-small red darken-1 btn-form">
                    <DeleteIcon/> {{ $t('delete' )}}
                </button>
            </div>
        </div>

        <div v-show="!editing" class="row">
            <p class="flow-text">
                {{ step.description }}
            </p>
        </div>

        <div v-show="editing">
            <Form @submit="updateDescription" :initial-values="step">
                <div class="row">
                    <StepDescriptionField/>
                </div>
                <div class="row">
                    <button class="waves-effect waves-light btn btn-form" type="submit">
                        <ConfirmIcon/> 
                        {{ $t('button.confirm' )}}
                    </button>
                    <button class="waves-effect waves-light btn btn-form grey darken-1" type="reset"
                    @click="editing = false">
                        <CancelIcon/> 
                        {{ $t('button.cancel' )}}
                    </button>
                </div>
            </Form>
        </div>
    </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
import { Form } from 'vee-validate'
import StepDescriptionField from './StepDescriptionField.vue'

import DeleteIcon from 'icons/AlertCircle'
import EditIcon from 'icons/Pencil'
import ConfirmIcon from 'icons/Check'
import CancelIcon from 'icons/CloseCircle'

export default {
    name: "StepItem",
    props: ["number", "step"],
    components: {
        Form, StepDescriptionField,
        EditIcon, DeleteIcon,
        ConfirmIcon, CancelIcon
    },
    data() {
        return {
            editing: false
        };
    },
    computed: {
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) },
    },
    methods: {
        ...mapActions([
            "deleteStep",
            "updateStep"
        ]),
        removeStep() {
            let id = this.step.id;
            this.deleteStep({ id });
        },
        updateDescription(values, { resetForm }) {
            let payload = {}
            payload.stepId = this.step.id
            payload.languageId = this.languageId
            payload.description = values.description
            this.updateStep(payload)
            .then(() => {
                this.editing = false
                resetForm()
            })
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "title": "step № {num}",
        "edit": "edit",
        "delete": "delete"
    },
    "ukr": {
        "title": "крок № {num}",
        "edit": "редагувати",
        "delete": "видалити"
    }
}
</i18n>