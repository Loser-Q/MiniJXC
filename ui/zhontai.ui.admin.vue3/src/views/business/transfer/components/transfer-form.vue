<template>
  <div>
    <el-dialog v-model="state.showDialog" destroy-on-close :title="state.form.id>0?'编辑调拨单':'新增调拨单'" draggable :close-on-click-modal="false" width="750px">
      <el-form ref="formRef" :model="state.form" label-width="80px">
        <el-row :gutter="20">
          <el-col :xs="24" :sm="8"><el-form-item label="调出仓库" prop="fromWarehouseId" :rules="[{required:true,message:'请选择'}]"><el-input-number v-model="state.form.fromWarehouseId" :min="1" style="width:100%" /></el-form-item></el-col>
          <el-col :xs="24" :sm="8"><el-form-item label="调入仓库" prop="toWarehouseId" :rules="[{required:true,message:'请选择'}]"><el-input-number v-model="state.form.toWarehouseId" :min="1" style="width:100%" /></el-form-item></el-col>
          <el-col :xs="24" :sm="8"><el-form-item label="日期"><el-date-picker v-model="state.form.billDate" type="date" value-format="YYYY-MM-DD" style="width:100%" /></el-form-item></el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :xs="24"><el-form-item label="调拨明细"><el-button @click="onAddItem" icon="ele-Plus" size="small">添加明细行</el-button></el-form-item></el-col>
          <el-col :xs="24">
            <el-table :data="state.form.items" border size="small">
              <el-table-column prop="productId" label="商品ID" width="120"><template #default="scope"><el-input-number v-model="scope.row.productId" :min="1" size="small" style="width:100%" /></template></el-table-column>
              <el-table-column prop="quantity" label="数量" width="120"><template #default="scope"><el-input-number v-model="scope.row.quantity" :min="0" :precision="2" size="small" style="width:100%" /></template></el-table-column>
              <el-table-column label="操作" width="60"><template #default="scope"><el-button text type="danger" icon="ele-Delete" size="small" @click="state.form.items.splice(scope.$index,1)" /></template></el-table-column>
            </el-table>
          </el-col>
        </el-row>
        <el-row :gutter="20" class="mt15">
          <el-col :xs="24"><el-form-item label="备注"><el-input v-model="state.form.remark" type="textarea" /></el-form-item></el-col>
        </el-row>
      </el-form>
      <template #footer><el-button @click="state.showDialog=false">取消</el-button><el-button type="primary" @click="onSure" :loading="state.sureLoading">确定</el-button></template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup name="business/transfer/form">
import { TransferEntity } from '/@/api/admin/data-contracts'
import { TransferApi } from '/@/api/admin/Transfer'
import eventBus from '/@/utils/mitt'
import { ElMessage } from 'element-plus'

const formRef = useTemplateRef('formRef')
const state = reactive({ showDialog: false, sureLoading: false, form: {} as any })
const open = async (row: any = {}) => {
  if (row.id > 0) { const res = await new TransferApi().get({ id: row.id }); if (res?.data) state.form = res.data as any }
  else { state.form = { ...row, items: row.items || [] } }
  state.showDialog = true
}
const onAddItem = () => { if (!state.form.items) state.form.items = []; state.form.items.push({ productId: 0, quantity: 0 }) }
const onSure = () => {
  formRef.value!.validate(async (valid: boolean) => { if (!valid) return; state.sureLoading = true
    let res: any
    if (state.form.id > 0) res = await new TransferApi().update(state.form).catch(() => { state.sureLoading = false })
    else res = await new TransferApi().add(state.form).catch(() => { state.sureLoading = false })
    state.sureLoading = false
    if (res?.success) { ElMessage.success('成功'); eventBus.emit('refreshTransfer'); state.showDialog = false }
  })
}
defineExpose({ open })
</script>
